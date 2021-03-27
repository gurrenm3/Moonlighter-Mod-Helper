using UnityEngine;

namespace Moonlighter_Mod_Helper.Extensions
{
    public static class GameObjectExt
    {
        public static T GetComponent<T>(this GameObject gameObject, string componentPath) where T : Component
        {
            return gameObject.transform.Find(componentPath).GetComponent<T>();
        }

        public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
        {
            GameObject.Destroy(gameObject.GetComponent<T>());
        }

        public static void RemoveComponent<T>(this GameObject gameObject, Component component) where T : Component
        {
            GameObject.Destroy(component);
        }

        public static void RemoveComponentByPath<T>(this GameObject gameObject, string componentPath) where T : Component
        {
            GameObject.Destroy(gameObject.transform.Find(componentPath));
        }

        public static void RemoveComponentByName(this GameObject gameObject, string componentName)
        {
            GameObject.Destroy(gameObject.GetComponent(componentName));
        }
    }
}
