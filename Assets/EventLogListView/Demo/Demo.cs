using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using EventLogListView;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventLog.Add("Start");
        EventLog.Error("Error");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Add
    public void Add(InputField inputField)
    {
        EventLog.Add(inputField.text);
    }

    // Add
    public async void AddLoading(Slider slider)
    {
        var eventLog = EventLog.AddLoading("Loading...");
        await Task.Delay((int)(slider.value * 1000));
        eventLog.Done();
    }
}
