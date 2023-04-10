using UnityEngine;

[ExecuteAlways]
public class AspectKeeper : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private Vector2 _aspectVector;

    private void Update()
    {
        var screenAspect = Screen.width / (float)Screen.height;
        var targetAspect = _aspectVector.x / _aspectVector.y;

        var magRate = targetAspect / screenAspect;

        var viewportRect = new Rect(0, 0, 1, 1);

        if(magRate < 1)
        {
            viewportRect.width = magRate;
            viewportRect.x = 0.5f - viewportRect.width * 0.5f;
        }
        else
        {
            viewportRect.height = 1 / magRate;
            viewportRect.y = 0.5f - viewportRect.height * 0.5f;
        }

        _camera.rect = viewportRect;
    }
}
