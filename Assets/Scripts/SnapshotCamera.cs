﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SnapshotCamera : MonoBehaviour
{

	Camera snapCam;

	int resWidth = 1080;
	int resHeight = 1080; 

	void Awake(){
		snapCam = GetComponent<Camera>();
		if (snapCam.targetTexture == null){
			snapCam.targetTexture = new RenderTexture(resWidth, resHeight, 24);
		}
		else{
			resWidth = snapCam.targetTexture.width;
			resHeight = snapCam.targetTexture.height; 
		}
		// snapCam.gameObject.SetActive(false);
	}

    // Update is called once per frame

    public void CallTakeSnapshot()
    {
    	snapCam.gameObject.SetActive(true);
    }

    void Update(){
    	if(snapCam.gameObject.activeInHierarchy){
    		Texture2D snapshot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
    		snapCam.Render();
    		RenderTexture.active = snapCam.targetTexture;
    		snapshot.ReadPixels(new Rect(0,0,resWidth, resHeight), 0, 0);
    		byte[] bytes = snapshot.EncodeToPNG();
    		string fileName = SnapshotName();
    		System.IO.File.WriteAllBytes(fileName, bytes);
    		Debug.Log(string.Format("Screenshot to: {0}", fileName));
    		snapCam.gameObject.SetActive(false);
    	}
    }
    string SnapshotName()
    {
    	return string.Format("{0}/Resources/Sprites/snap_{1}x{2}_{3}.png", 
    		Application.dataPath,
    		resWidth,
    		resHeight,
    		System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
}


