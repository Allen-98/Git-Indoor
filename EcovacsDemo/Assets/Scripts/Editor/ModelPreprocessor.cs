using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ModelPreprocessor : AssetPostprocessor
{
    public void OnPreprocessModel()
    {
        //Debug.Log("ModelPreprocessor.OnPreprocessModel");

        ModelImporter modelImporter = (ModelImporter)assetImporter;
        modelImporter.animationCompression = ModelImporterAnimationCompression.Optimal;
        modelImporter.meshCompression = ModelImporterMeshCompression.Off;
        modelImporter.meshOptimizationFlags = MeshOptimizationFlags.Everything;
        modelImporter.weldVertices = true;
    }

    public void OnPostprocessAnimation(GameObject go, AnimationClip clip)
    {
        try
        {
            //去除scale曲线
            foreach (EditorCurveBinding theCurveBinding in AnimationUtility.GetCurveBindings(clip))
            {
                string name = theCurveBinding.propertyName.ToLower();
                if (name.Contains("scale"))
                {
                    AnimationUtility.SetEditorCurve(clip, theCurveBinding, null);
                }
            }

            string format = "f2";

            //浮点数精度压缩到f2
            AnimationClipCurveData[] curves = null;
            curves = AnimationUtility.GetAllCurves(clip);
            Keyframe key;
            Keyframe[] keyFrames;
            for (int ii = 0; ii < curves?.Length; ++ii)
            {
                AnimationClipCurveData curveDate = curves[ii];
                if (curveDate.curve == null || curveDate.curve.keys == null)
                {
                    continue;
                }
                keyFrames = curveDate.curve.keys;
                for (int i = 0; i < keyFrames.Length; i++)
                {
                    key = keyFrames[i];
                    key.value = float.Parse(key.value.ToString(format));
                    key.inTangent = float.Parse(key.inTangent.ToString(format));
                    key.outTangent = float.Parse(key.outTangent.ToString(format));
                    keyFrames[i] = key;
                }
                curveDate.curve.keys = keyFrames;
                clip.SetCurve(curveDate.path, curveDate.type, curveDate.propertyName, curveDate.curve);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"CompressAnimationClip Failed !!! animationPath : {assetPath} error: {e}");
        }
    }
}

