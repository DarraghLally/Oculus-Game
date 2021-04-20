using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false; // show controller or hands
    public InputDeviceCharacteristics controllerCharacteristic;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialise();
    }

    void TryInitialise()
    {
        // Get list of connected devices
        List<InputDevice> devices = new List<InputDevice>();
        //InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller; // Right controller
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, devices);
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0]; // just get first device
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                // If we find a match, instanciate it
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Did not find controller model");
                spawnedController = Instantiate(controllerPrefabs[0], transform); // Give default controller (first in list)
            }
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimaton() 
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) 
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }


    }

    void Update()
    {
        /*
         * DEBUGs ->
         * 
        // Primary button press        
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue)  && primaryButtonValue) //first check we have a value, then use it
            Debug.Log("Primary button pressed");
        // Trigger button press       
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1)
            Debug.Log("Trigger button pressed " + triggerValue);
        // Touchpad press (analog stick)       
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
            Debug.Log("Trigger button pressed " + primary2DAxisValue);
        */
        if (!targetDevice.isValid)
        {
            TryInitialise();
        }
        else
        {
            if (showController)
            {
                // show controller
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                // show hands
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimaton();
            }
        }        
    }
}
