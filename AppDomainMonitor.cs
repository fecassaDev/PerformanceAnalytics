public class AppDomainMonitor
{
    public AppDomain TargetAppDomain { get; private set; }
    private TimeSpan InitialProcessorTimeField;
    private long InitialAllocatedMemorySizeField;
    private long InitialSurvivedMemorySize;

    public AppDomainMonitor(AppDomain targetAppDomain = null)
    {
        AppDomain.MonitoringIsEnabled = true;
        this.TargetAppDomain = targetAppDomain ?? AppDomain.CurrentDomain;

        this.Reset();
    }

    public void Reset()
    {
        this.InitialProcessorTimeField = this.TargetAppDomain.MonitoringTotalProcessorTime;
        this.InitialAllocatedMemorySizeField = this.TargetAppDomain.MonitoringTotalAllocatedMemorySize;
        this.InitialSurvivedMemorySize = this.TargetAppDomain.MonitoringSurvivedMemorySize;
    }

    public AppDomainMonitorSnapshot TakeSnapshot()
    {
        return new AppDomainMonitorSnapshot(this);
    }

    public struct AppDomainMonitorSnapshot
    {
        public string AppDomainFriendlyName { get; private set; }
        public double ProcessorTimeMs { get; private set; }
        public long AllocatedMemorySize { get; private set; }
        public long SurvivedMemorySize { get; private set; }

        public AppDomainMonitorSnapshot(AppDomainMonitor m) : this()
        {
            if (m == null)
                throw new ArgumentNullException();

            GC.Collect();

            this.AppDomainFriendlyName = m.TargetAppDomain.FriendlyName;
            this.ProcessorTimeMs = (m.TargetAppDomain.MonitoringTotalProcessorTime - m.InitialProcessorTimeField).TotalMilliseconds;
            this.AllocatedMemorySize = m.TargetAppDomain.MonitoringTotalAllocatedMemorySize - m.InitialAllocatedMemorySizeField;
            this.SurvivedMemorySize = m.TargetAppDomain.MonitoringSurvivedMemorySize - m.InitialSurvivedMemorySize;
        }

        public override string ToString()
        {
            return string.Format(

           "AppDomain Friendly-name={0}, CPU={1}, \nAllocated MemorySize={2:N0}, Survived = {3:N0}",
           this.AppDomainFriendlyName,
           this.ProcessorTimeMs,
           this.AllocatedMemorySize,
           this.SurvivedMemorySize);

        }
    }
}
