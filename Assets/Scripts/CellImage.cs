using UnityEngine;

public class CellImage: MonoBehaviour
{

    private SpriteRenderer _sprite;

    void Awake()
    {
        GlobalEvent.Subscribe(TypesEvent.NewValue, ChangeSprite);
        _sprite=transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void ChangeSprite()
    {
        _sprite.sprite = Resources.Load<Sprite>("Sprites/"+name);
    }

    private void OnDestroy()
    {
        GlobalEvent.Unsubscribe(TypesEvent.NewValue, ChangeSprite);
    }


}
