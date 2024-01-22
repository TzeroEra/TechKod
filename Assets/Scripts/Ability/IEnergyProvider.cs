public interface IEnergyProvider
{
    float CurrentEnergy { get; }
    bool CanConsumeEnergy(float amount);
    void ConsumeEnergy(float amount);
}