using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{

    public class Task
    {
        public string Text;
        public int ID;
        private GameObject TaskObject;

        public Task(string text, int id, GameObject TaskPrefab)
        {
            ID = id;
            Text = text;
            TaskObject = Instantiate(TaskPrefab);
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
        ActiveTasks.Add(new Task(text, id, TaskPrefab));
    }

    public void StopTask(int id)
    {
       Task Tasktodelete = ActiveTasks[id];
        Tasktodelete.Delete();
    }
}
