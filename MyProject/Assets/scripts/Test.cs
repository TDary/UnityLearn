using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Application.LogCallback logcall;
    // Start is called before the first frame update
    void Start()
    {
#if USE_WEST
        this.gameObject.AddComponent<WeTest.U3DAutomation.U3DAutomationBehaviour>();
        BuglyAgent.RegisterLogCallback(WeTest.U3DAutomation.CrashMonitor._OnLogCallbackHandler);
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
