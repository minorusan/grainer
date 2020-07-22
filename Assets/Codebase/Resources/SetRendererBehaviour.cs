using UnityEngine;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class SetRendererBehaviour : MonoBehaviour
{
    public UniversalRenderPipelineAsset Renderer;

    private void Start()
    {
        QualitySettings.renderPipeline = Renderer;
    }
}
