using UnityEngine;

public class BackgroundController : MonoBehaviour {

    #region Properties

    [Header("Properties")]

    [SerializeField]
    private Background _background;

    /// <summary>
    /// X => Left
    /// Y => Right
    /// Z => Top
    /// W => Bottom
    /// </summary>
    private Vector4 _borders;

    private Vector3 _border_exit = Vector3.zero;
    private Vector3 _border_entry = Vector3.zero;

    private bool _isDirectionChanged = false;

    #endregion

    #region Settings

    [Header("Settings")]

    [SerializeField]
    private bool _isScrolling = true;

    [SerializeField]
    private Vector2 _direction = new Vector2(-1, 0);

    [SerializeField]
    private float _speed = 1;

    #endregion

    /**************************************************/

    #region Start

    private void Start() {
        Init();
    }

    #endregion

    #region Init

    private void Init() {
        InitBorders();

        _background.Init();
    }

    private void InitBorders() {
        float distance = (_background.transform.position - Camera.main.transform.position).z;

        float left = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x;
        float right = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x;
        float top = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, distance)).y;
        float bottom = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).y;

        _borders = new Vector4(left, right, top, bottom);

        SetBordersExitAndEntry();
    }

    #endregion


    private void Update() {
        if (!AllowScroll()) {
            return;
        }

        InitBorders();

        MoveBackground();

        CheckKeyboardInputs();

        if (_isDirectionChanged) {
            SetBordersExitAndEntry();
            _isDirectionChanged = false;
        }
    }


    #region Background: Move

    private void MoveBackground() {
        Vector3 newPosition = new Vector3(_speed * _direction.x, _speed * _direction.y, 0);
        newPosition *= Time.deltaTime;

        _background.transform.Translate(newPosition);

        _background.CheckParts(_direction, _border_exit);
    }

    #endregion


    #region Set: Direction

    public void SetDirection(BackgroundDirection newDirection) {
        switch (newDirection) {
            case BackgroundDirection.RightToLeft:
                _direction = new Vector2(-1, 0);
                break;

            case BackgroundDirection.LeftToRight:
                _direction = new Vector2(1, 0);
                break;

            case BackgroundDirection.TopToBottom:
                _direction = new Vector2(0, -1);
                break;

            case BackgroundDirection.BottomToTop:
                _direction = new Vector2(0, 1);
                break;
        }

        _isDirectionChanged = true;
    }

    #endregion

    

    #region Allow: Scroll

    private bool AllowScroll() {
        return _isScrolling;
    }

    #endregion


    #region Borders: Set Exit and Entry

    private void SetBordersExitAndEntry() {
        if (_direction.x < 0) {
            _border_exit.x = _borders.x;
            _border_entry.x = _borders.y;

        } else if (_direction.x > 0) {
            _border_exit.x = _borders.y;
            _border_entry.x = _borders.x;
        }

        if (_direction.y < 0) {
            _border_exit.y = _borders.w;
            _border_entry.y = _borders.z;

        } else if (_direction.y > 0) {
            _border_exit.y = _borders.z;
            _border_entry.y = _borders.w;
        }
    }

    #endregion


    #region CheckInputs

    // Dummy
    private void CheckKeyboardInputs() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            SetDirection(BackgroundDirection.BottomToTop);

        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            SetDirection(BackgroundDirection.TopToBottom);

        } else if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            SetDirection(BackgroundDirection.RightToLeft);

        } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
            SetDirection(BackgroundDirection.LeftToRight);

        }
    }

    #endregion

}