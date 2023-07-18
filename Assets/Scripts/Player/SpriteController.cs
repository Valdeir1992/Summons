using System.Threading.Tasks;
using UnityEngine;

public class SpriteController : MonoBehaviour 
{ 
    private SpriteRenderer _spriteRender;

    private void Awake()
    {
        _spriteRender = transform.GetChild(0).GetComponent<SpriteRenderer>(); 
    }

    public async void TakeDamage()
    {
        _spriteRender.color = Color.red + Color.white/2;
        await Task.Delay(100);
        _spriteRender.color = Color.white;
    }


    public void TurnToLeft() 
    {
        if (_spriteRender.flipX) return;
        _spriteRender.flipX = true;
    }
    public void TurnToRight()
    {
        if (!_spriteRender.flipX) return;
        _spriteRender.flipX = false;
    }
}
