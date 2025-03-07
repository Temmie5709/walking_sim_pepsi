using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TaskManager : MonoBehaviour
{

    public class Task
    {
        public string Text;
        public int ID;
        private GameObject TaskObject;

        public Task(string text, int id, GameObject TaskPrefab,GameObject parent)
        {
            ID = id;
            Text = text;
            TaskObject = Instantiate(TaskPrefab);
            TaskObject.transform.SetParent(parent.transform);
            TaskObject.GetComponent<TextMeshProUGUI>().SetText(text);
        }

       public void Delete()
       {
            GameObject.Destroy(TaskObject);
       }
    }


    public List<Task> ActiveTasks = new List<Task>();
    [SerializeField] GameObject TaskPrefab;

    public void CreateTask(string text, int id)
    {
        ActiveTasks.Add(new Task(text, id, TaskPrefab,gameObject));
    }

    public void StopTask(int id)
    {
        ActiveTasks.RemoveAll(task =>
        {
            if (task.ID == id)
            {
                task.Delete();
                return true; // Supprime l'élément de la liste
            }
            return false; // Conserve l'élément
        });
    }
}
