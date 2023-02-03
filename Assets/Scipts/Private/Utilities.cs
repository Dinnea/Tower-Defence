using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Personal.Utilities
{
    public static class Utilities
    {
       // public const int sortingOrderDefault = 5000;

        // Create Text in the World
        public static TextMesh CreateTextInWorld(string text, Transform parent = null, Vector3 localPosition = default, int fontSize = 40, Color? color = null, 
                                               Vector3 rotation = default, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignment textAlignment = TextAlignment.Left)//, int sortingOrder = sortingOrderDefault)
        {
            if (color == null) color = Color.white;
            return CreateTextInWorld(parent, text, localPosition, fontSize, (Color)color, rotation, textAnchor, textAlignment);//, sortingOrder);
        }


        public static TextMesh CreateTextInWorld(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, 
                                               Vector3 rotation, TextAnchor textAnchor, TextAlignment textAlignment)//, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            transform.rotation = Quaternion.Euler(rotation);
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
           // textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }

        public static Vector3 GetMousePositionWorld(Camera camera, LayerMask layermask)
        {            
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layermask))
            {
                return raycastHit.point;
            }
            return default;

        }
      /*  //get mouse position in world
        public static Vector3 GetMouseInWorldPositionXZ() 
        {
            Vector3 worldPosition = GetInWorldPosition3D(Input.mousePosition, Camera.main);
            worldPosition.y = 0f;
            Debug.Log(worldPosition);
            //worldPosition
            return worldPosition;
        }

        public static Vector3 GetMouseInWorldPosition3D() //mouse posiiton in world by default main camera
        {
            return GetInWorldPosition3D(Input.mousePosition, Camera.main);
        }
        public static Vector3 GetMouseInWorldPosition3D(Camera camera) //mouse position, choose camera
        {
            return GetInWorldPosition3D(Input.mousePosition, camera);
        }
        public static Vector3 GetInWorldPosition3D(Vector3 screenPosition, Camera camera)  //screen to world position
        {
           Ray ray = camera.ScreenPointToRay(Input)
            return worldPosition;
        }*/
    }
}

