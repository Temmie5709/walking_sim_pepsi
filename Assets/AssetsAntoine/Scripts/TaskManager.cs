using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
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
            TaskObject = Instantiate(TaskPrefab,parent.transform);
            TaskObject.GetComponent<TextMeshProUGUI>().SetText(text);
        }

       public void Delete()
       {
            Destroy(TaskObject);
       }
    }


    public List<Task> ActiveTasks = new List<Task>();
    [SerializeField] GameObject TaskPrefab;
    string Text = ""; 

    public void setTaskText(string text)
    {
        Text = text;
    }
    public void CreateTask(int id)
    {
        ActiveTasks.Add(new Task(Text, id, TaskPrefab,gameObject));
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
