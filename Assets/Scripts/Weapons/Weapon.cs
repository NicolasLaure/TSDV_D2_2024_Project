using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Events;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected FireMode fireMode;
    private Coroutine _fireCoroutine;

    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private GameObject environmentWeaponPrefab;
    [SerializeField] private Animator weaponAnimator;

    [Header("Recoil")]
    [SerializeField] private float recoilRecoverTime;
    [SerializeField] private Vector3EventChannelSO recoilAngleEventChannel;
    [SerializeField] private FloatChannelEvent resetRecoil;
    // To rotate the camera I could calculate the new camera forward
    // based on that I could get camera new rotation based on the quaternion that has this new result as forward

    [SerializeField] public UnityEvent onShoot;
    [SerializeField] public UnityEvent onReload;
    [SerializeField] public UnityEvent onReloadFinished;
    [Header("Visuals")]
    [SerializeField] protected DecalsHandler decals;

    [SerializeField] public Transform pivot;

    private bool _isReloading = false;
    private int _currentMagazine;

    private bool _isFiring = false;
    private Coroutine _fullAutoCoroutine;

    private float _originXAngle;
    private float _originYAngle;
    private Coroutine _recoilDecayCoroutine;

    [SerializeField] private bool hasSpread;
    protected int consecutiveShots = 0;
    public float LastShotTime { get; set; }

    public int CurrentAmmo
    {
        get { return _currentMagazine; }
    }

    public WeaponSO WeaponSO
    {
        get { return weaponSO; }
    }

    public GameObject EnvironmentWeaponPrefab
    {
        get { return environmentWeaponPrefab; }
    }

    protected void Awake()
    {
        _currentMagazine = weaponSO.MagazineSize;
        decals = GameObject.FindObjectOfType<DecalsHandler>();
        weaponSO.onMagChanged += OnMagazineChanged;

        if (pivot == null)
            pivot = Camera.main.transform;
    }

    private void OnEnable()
    {
        _isReloading = false;
        _isFiring = false;
    }

    private void OnDestroy()
    {
        weaponSO.onMagChanged -= OnMagazineChanged;
    }

    public void Shoot()
    {
        if (!CanShoot())
        {
            if (!HasBullets())
                Reload();
            return;
        }

        if (_fireCoroutine == null)
        {
            if (_recoilDecayCoroutine != null)
            {
                StopCoroutine(_recoilDecayCoroutine);
                _recoilDecayCoroutine = null;
            }

            _isFiring = true;
            _fireCoroutine = StartCoroutine(fireMode.Fire(this));
        }
    }

    public void StopShooting()
    {
        if (_fireCoroutine != null)
        {
            _isFiring = false;
            StopCoroutine(_fireCoroutine);
            _fireCoroutine = null;
            _recoilDecayCoroutine = StartCoroutine(ResetRecoil());
        }
    }

    public void Reload()
    {
        if (!_isReloading)
            StartCoroutine(ReloadCoroutine());
    }

    public virtual void FireWeapon()
    {
        if (Time.time - LastShotTime < weaponSO.RecoilDecay)
            consecutiveShots++;
        else
            consecutiveShots = 0;

        LastShotTime = Time.time;
        _currentMagazine--;
        onShoot?.Invoke();
    }

    public bool CanShoot()
    {
        return HasBullets() && !(_isReloading) && !OnCoolDown();
    }

    public bool HasBullets()
    {
        return _currentMagazine > 0;
    }

    private IEnumerator ReloadCoroutine()
    {
        onReload?.Invoke();
        _isReloading = true;
        yield return new WaitForSeconds(weaponSO.ReloadingDuration);
        _isReloading = false;
        _currentMagazine = weaponSO.MagazineSize;
        onReloadFinished?.Invoke();
    }

    private void OnMagazineChanged()
    {
        _currentMagazine = weaponSO.MagazineSize;
        if (this.isActiveAndEnabled)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    public void OnMovementChange(Vector2 dir)
    {
        if (dir != Vector2.zero)
            weaponAnimator.SetBool("isWalking", true);
        else
            weaponAnimator.SetBool("isWalking", false);
    }

    public void OnSprintChange(bool value)
    {
        weaponAnimator.SetBool("isSprinting", value);
    }

    //private IEnumerator BurstAuto()
    //{
    //    isFiring = true;
    //    do
    //    {
    //        yield return StartCoroutine(Burst());
    //        yield return StartCoroutine(ShootCoolDown());
    //    } while (isFiring && CanShoot());
    //}
    private bool OnCoolDown()
    {
        return LastShotTime + weaponSO.ShootingCoolDown > Time.time;
    }

    public IEnumerator ShootCoolDown()
    {
        yield return new WaitForSeconds(weaponSO.ShootingCoolDown);
    }

    public Vector3 BulletSpread(float shotCount)
    {
        if (!hasSpread)
            return pivot.forward;

        float spreadAmount = weaponSO.SpreadCurve.Evaluate(shotCount * weaponSO.SpreadGrowth);
        float horizontalSpread = weaponSO.MaxHorizontalSpread * spreadAmount;
        float verticalSpread = weaponSO.MaxVerticalSpread * spreadAmount;

        float randomXAngle = Random.Range(-verticalSpread, verticalSpread);
        float randomYAngle = Random.Range(-horizontalSpread, horizontalSpread);

        Quaternion rotation = Quaternion.Euler(randomXAngle, randomYAngle, 0);

        return rotation * pivot.forward;
    }

    public void RecoilCameraDisplacement(float progress)
    {
        if (recoilAngleEventChannel == null)
            return;

        if (progress == 0)
        {
            _originXAngle = transform.rotation.eulerAngles.x;

            _originYAngle = transform.rotation.eulerAngles.y;
            recoilAngleEventChannel.RaiseEvent(new Vector3(_originXAngle, _originYAngle, 0));
            return;
        }

        float newXAxisAngle = -weaponSO.VerticalDisplacement.Evaluate(progress);
        float newYAxisAngle = -weaponSO.HorizontalDisplacement.Evaluate(progress);

        recoilAngleEventChannel.RaiseEvent(new Vector3(newXAxisAngle, newYAxisAngle, progress));
        transform.localRotation = Quaternion.Euler(_originXAngle + newXAxisAngle, _originYAngle + newYAxisAngle, 0);
    }

    public IEnumerator ResetRecoil()
    {
        if (resetRecoil == null)
            yield break;

        float timer = 0;
        float startTime = Time.time;
        while (timer < weaponSO.RecoilDecay)
        {
            timer = Time.time - startTime;
            resetRecoil.RaiseEvent(timer / weaponSO.RecoilDecay);
            Vector3 cameraRotationEulers = Camera.main.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraRotationEulers.x, cameraRotationEulers.y, transform.rotation.eulerAngles.z);
            yield return null;
        }
    }
}