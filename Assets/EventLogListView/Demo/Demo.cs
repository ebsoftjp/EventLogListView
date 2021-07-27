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

    // Add loading
    public async void AddLoading(Slider slider)
    {
        var eventLog = EventLog.AddLoading("Loading...");
        await Task.Delay((int)(slider.value * 1000));
        eventLog.Done("Success");
    }

    // Add loading error
    public async void AddLoadingError(Slider slider)
    {
        var eventLog = EventLog.AddLoading("Loading...");
        await Task.Delay((int)(slider.value * 1000));
        eventLog.Failed("Failed: error message here");
    }

    // Add error
    public void Error(InputField inputField)
    {
        EventLog.Error(inputField.text);
    }

    // Add notification
    public void Notification(InputField inputField)
    {
        EventLog.Notification(inputField.text);
    }
}
