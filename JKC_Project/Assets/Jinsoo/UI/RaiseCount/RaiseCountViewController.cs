using UnityEngine;

public class RaiseCountViewController : ViewController
{
    private void Awake()
    {
        View = transform.Find("RaiseCountView").GetComponent<RaiseCountView>();
        Debug.Assert(View != null);
        Presenter = new RaiseCountPresenter();
        Debug.Assert(Presenter != null);
    }
}
