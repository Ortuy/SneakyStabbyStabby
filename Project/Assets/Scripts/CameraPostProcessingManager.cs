using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPostProcessingManager : MonoBehaviour
{
    private UnityEngine.Rendering.Universal.UniversalAdditionalCameraData mainCameraExtraData;

    // Start is called before the first frame update
    void Start()
    {
        mainCameraExtraData = GetComponent<UnityEngine.Rendering.Universal.UniversalAdditionalCameraData>();
        UpdatePostProcessing();
    }

    public void UpdatePostProcessing()
    {
        bool postProcessingOn = PlayerPrefs.GetInt("postprocess") == 1 ? true : false;
        mainCameraExtraData.renderPostProcessing = postProcessingOn;
    }
}
