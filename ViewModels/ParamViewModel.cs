using CommunityToolkit.Mvvm.ComponentModel;

namespace NavigationQueryTest.ViewModels;

public partial class ParamViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private string param;

    [ObservableProperty]
    private string appearedAt;

    [ObservableProperty]
    private string navigatedAt;

    [ObservableProperty]
    private string receivedParamAt;

    [ObservableProperty]
    private string eventsReport;

    public TimeProvider _timeProvider { get; }

    private Dictionary<string, DateTimeOffset> eventTimes = new();

    public ParamViewModel()
    {
        _timeProvider = TimeProvider.System;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("param", out var param))
        {
            Param = (string)param;

            var tod = _timeProvider.GetUtcNow();

            ReceivedParamAt = $"Received navigation param at {tod.TimeOfDay}";

            LogEvent("Received navigation parameters", tod);
        }
    }

    public void Appearing()
    {
        var tod = _timeProvider.GetUtcNow();

        AppearedAt = $"Appeared at {tod.TimeOfDay}";

        LogEvent("Appeared", tod);
    }

    public void Navigated()
    {
        var tod = _timeProvider.GetUtcNow();

        NavigatedAt = $"Navigated at {tod.TimeOfDay}";

        LogEvent("Navigated to", tod);
    }

    private void LogEvent(string eventName, DateTimeOffset eventTime)
    {
        eventTimes.Add(eventName, eventTime);

        if (eventTimes.Count == 3)
        {
            GenerateReport();
        }
    }

    private void GenerateReport()
    {
        var sortedTimes = eventTimes.OrderBy(t => t.Value).ToList();
        string report = "";

        for (int i = 0; i < sortedTimes.Count; i++)
        {
            var sortedTime = sortedTimes[i];

            if (i > 0)
            {
                var previousSortedTime = sortedTimes[i - 1];
                var diff = sortedTime.Value - previousSortedTime.Value;
                report += $"{sortedTime.Key} happened {diff.TotalMilliseconds} milliseconds after {previousSortedTime.Key}\n";
            }
            else
            {
                report += $"{sortedTime.Key} is the first event\n";
            }
        }

        EventsReport = report;
    }
}