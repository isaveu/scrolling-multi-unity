using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    #region Properties

    [Header("Properties")]

    [SerializeField]
    private BackgroundPart _partPrefab;

    private Vector2 _size;
    public Vector2 Size {
        get {
            return _size;
        }
    }

    #endregion

    private List<BackgroundPart> _parts;
    private List<BackgroundPart> _repositionParts;

    /**************************************************/

    #region Init

    public void Init() {
        CreateParts();

        InitSize();
    }

    private void InitSize() {
        //_size = new Vector2(Mathf.Abs(_partPrefab.Sprite.size.x * 3), Mathf.Abs(_partPrefab.Sprite.size.y * 3));
        BackgroundPart bg_first = _parts[0];
        BackgroundPart bg_last = _parts[_parts.Count - 1];

        float x = bg_last.transform.position.x - bg_first.transform.position.x;
        float y = bg_last.transform.position.y - bg_first.transform.position.y;

        _size = new Vector2(Mathf.Abs(x), Mathf.Abs(y));
    }

    #endregion


    #region Check: RePosition

    public void CheckParts(Vector2 direction, Vector2 borderExit) {
        bool allowReposition = AllowRePositionParts(direction, borderExit);
        if (allowReposition) {
            RePositionParts(direction);
        }
    }

    #endregion


    #region Parts: AllowRePosition

    private bool AllowRePositionParts(Vector2 direction, Vector2 borderExit) {
        _repositionParts = new List<BackgroundPart>();

        foreach (BackgroundPart part in _parts) {
            if (direction.x != 0) {
                if ((direction.x < 0 && (part.transform.position.x + (_partPrefab.Sprite.size.x * .5f) < borderExit.x)) ||
                    (direction.x > 0 && (part.transform.position.x - (_partPrefab.Sprite.size.x * .5f) > borderExit.x))) {

                    _repositionParts.Add(part);
                }
            }

            if (direction.y != 0) {
                if ((direction.y < 0 && (part.transform.position.y + (_partPrefab.Sprite.size.y * .5f) < borderExit.y)) || 
                    (direction.y > 0 && (part.transform.position.y - (_partPrefab.Sprite.size.y * .5f) > borderExit.y))) {

                    _repositionParts.Add(part);
                }
            }
        }

        return _repositionParts.Count > 0 ? true : false;
    }

    #endregion

    #region Parts: RePosition

    private void RePositionParts(Vector2 direction) {
        foreach (BackgroundPart part in _repositionParts) {
            float x = part.transform.position.x + ((_size.x + part.Sprite.size.x) * -1 * direction.x);
            float y = part.transform.position.y + ((_size.y + part.Sprite.size.y) * -1 * direction.y);
            float z = part.transform.position.z;

            part.transform.position = new Vector3(x, y, z);
        }
    }

    #endregion


    #region Parts: Create

    private void CreateParts() {
        _parts = new List<BackgroundPart> {
            CreatePart(BackgroundPartId.Top_Left),
            CreatePart(BackgroundPartId.Top),
            CreatePart(BackgroundPartId.Top_Right),
            CreatePart(BackgroundPartId.Center_Left),
            CreatePart(BackgroundPartId.Center),
            CreatePart(BackgroundPartId.Center_Right),
            CreatePart(BackgroundPartId.Bottom_Left),
            CreatePart(BackgroundPartId.Bottom),
            CreatePart(BackgroundPartId.Bottom_Right),
        };
    }

    private BackgroundPart CreatePart(BackgroundPartId partId) {
        BackgroundPart part = Instantiate(_partPrefab) as BackgroundPart;
        part.Init(partId, this.transform);

        return part;
    }

    #endregion

}