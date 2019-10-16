namespace MyElectricTeapot
{
    public class WaterContainer
    {
        // коофициент теплопередачи в окружающую среду
        protected const float HEAT_OUT_CONST = 2;
        // Вода внутри контейнера
        public Water InnerWater { get; private set; }
        // Максимальный объём контейнера. Так как автор садист, то не MaxVolume
        public int Volume { get; }

        public WaterContainer(int volume = 2000)
        {
            if (volume > 500 && volume < 5000)
            {
                Volume = volume;
            }
            else
            {
                throw new System.ArgumentException();
            }
        }

        // Потеря тепла из-за окружающей среды
        public void Update(float timedelta)
        {
            float coldQuantity = HEAT_OUT_CONST * (InnerWater.GetTemperature() - Teapot.OUTSIDE_TEMPERATURE) * timedelta;
            InnerWater = InnerWater.ChangeTemperature(-coldQuantity);
        }
        // Изменение температуры воды
        public void Update(float timedelta, float heatQuantity)
        {
            float coldQuantity = HEAT_OUT_CONST * (InnerWater.GetTemperature() - Teapot.OUTSIDE_TEMPERATURE) * timedelta;
            InnerWater = InnerWater.ChangeTemperature(heatQuantity - coldQuantity);
        }

        // Налить воду
        public void AddWater(Water amount) 
        {
            if (InnerWater.waterAmount + amount.waterAmount > Volume)
            {
                Water waterPart = new Water(Volume - InnerWater.waterAmount, amount.GetTemperature());
                InnerWater += waterPart;
            }
            else
            {
                InnerWater += amount;
            }
        }

        // Вылить всю воду
        public Water PourWater() 
        {
            Water inner = InnerWater;
            InnerWater = new Water();
            return inner;
        }
        // Вылить часть воды
        public Water PourWater(int amount)
        {
            Water inner = InnerWater;
            InnerWater -= amount;
            return new Water(inner.waterAmount - InnerWater.waterAmount, inner.GetTemperature());
        }

        // Являеться ли контейнер пустым
        public bool IsEmpty() => InnerWater.waterAmount == 0;
        // Являеться ли контейнер полным
        public bool IsFull() => InnerWater.waterAmount == Volume;
    }
}