using UnityEngine;

public class AndroidBackButtonOnPressBehaviour : MonoBehaviour
{
    [SerializeField] private OnPressButtonActionBase action;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (action!=null)
            {
                action.OnPressBackButton(this.gameObject);
            }
        }
    }
}