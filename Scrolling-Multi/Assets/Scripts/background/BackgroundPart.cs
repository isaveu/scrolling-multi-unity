using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BackgroundPart : MonoBehaviour {

    #region Components

    [Header("Components")]

    /// <summary>
    /// SpriteRenderer
    /// </summary>
    [SerializeField]
    private SpriteRenderer _sprite;
    public SpriteRenderer Sprite {
        get {
            return _sprite;
        }
    }

    #endregion

    #region Properties

    [Header("Properties")]

    [SerializeField]
    private BackgroundPartId _id = BackgroundPartId.NONE;

    #endregion

    /**************************************************/

    #region Init

    public void Init(BackgroundPartId id, Transform parent) {
        this._id = id;

        this.name = "part_" + id.ToString();
        this.transform.SetParent(parent);

        InitPosition(id);

        // SetColor(id);
    }

    private void InitPosition(BackgroundPartId partId) {
        Vector2 position = Vector2.zero;

        switch (partId) {
            case BackgroundPartId.Top_Left:
                position = new Vector2(-_sprite.size.x, _sprite.size.y);
                break;

            case BackgroundPartId.Top:
                position = new Vector2(0, _sprite.size.y);
                break;

            case BackgroundPartId.Top_Right:
                position = new Vector2(_sprite.size.x, _sprite.size.y);
                break;

            case BackgroundPartId.Center_Left:
                position = new Vector2(-_sprite.size.x, 0);
                break;

            case BackgroundPartId.Center:
                position = new Vector2(0, 0);
                break;

            case BackgroundPartId.Center_Right:
                position = new Vector2(_sprite.size.x, 0);
                break;

            case BackgroundPartId.Bottom_Left:
                position = new Vector2(-_sprite.size.x, -_sprite.size.y);
                break;

            case BackgroundPartId.Bottom:
                position = new Vector2(0, -_sprite.size.y);
                break;

            case BackgroundPartId.Bottom_Right:
                position = new Vector2(_sprite.size.x, -_sprite.size.y);
                break;
        }

        this.transform.position = position;
    }

    #endregion

    #region Set: Color (for debug)
    
    private void SetColor(BackgroundPartId id) {
        switch (id) {
            case BackgroundPartId.Top_Left:
                _sprite.color = Color.white;
                break;
            case BackgroundPartId.Top:
                _sprite.color = Color.yellow;
                break;
            case BackgroundPartId.Top_Right:
                _sprite.color = Color.magenta;
                break;
            case BackgroundPartId.Center_Left:
                _sprite.color = Color.white;
                break;
            case BackgroundPartId.Center:
                _sprite.color = Color.yellow;
                break;
            case BackgroundPartId.Center_Right:
                _sprite.color = Color.magenta;
                break;
            case BackgroundPartId.Bottom_Left:
                _sprite.color = Color.white;
                break;
            case BackgroundPartId.Bottom:
                _sprite.color = Color.yellow;
                break;
            case BackgroundPartId.Bottom_Right:
                _sprite.color = Color.magenta;
                break;
        }
    }

    #endregion

}