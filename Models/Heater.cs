namespace MyElectricTeapot
{
    public class Heater
    {
        public int Power { get; protected set; }
        public float Efficioncy { get; }

        public Heater(int power = 1500, float efficioncy = 0.85f)
        {
            if (power < 200 || power > 10000)
            {
                throw new System.ArgumentException();
            }

            if (efficioncy <= 0 || efficioncy > 1)
            {
                throw new System.ArgumentException();
            }

            Power = power;
            Efficioncy = efficioncy;
        }

        public float Work(float timedelta)
        {
            return Power * Efficioncy * timedelta;
        }
    }
}