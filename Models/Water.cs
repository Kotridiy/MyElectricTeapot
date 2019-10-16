namespace MyElectricTeapot
{
    public struct Water
    {
        // Количество воды
        public readonly float waterAmount;
        // Температура воды
        private float temperature;
        // Удельная теплоёмкость
        const int waterC = 4200;
        // Удельная величина парообразования
        const int waterL = 2258;

        public Water(float waterAmount, float temperature)
        {
            if (waterAmount < 0)
            {
                throw new System.ArgumentException();
            }
            if (temperature < -273)
            {
                throw new System.ArgumentException();
            }

            this.waterAmount = waterAmount;
            this.temperature = temperature;
        }

        public static Water operator +(Water waterA, Water waterB)
        {
            float sumAmount = waterA.waterAmount + waterB.waterAmount;
            if (sumAmount == 0)
            {
                return new Water();
            }
            float avgTemperature = (waterA.waterAmount * waterA.temperature + waterB.waterAmount * waterB.temperature) / sumAmount;
            return new Water(sumAmount, avgTemperature); 
        } 
        public static Water operator -(Water water, int amount)
        {
            if (amount > water.waterAmount)
            {
                return new Water();
            }
            else
            {
                return new Water(water.waterAmount - amount, water.temperature);
            }
        }

        // Изменить температуру сообщив некоторое количество теплоты
        public Water ChangeTemperature(float heatQuantity)
        {
            if (waterAmount == 0) return this;
            if (temperature >= 100 && heatQuantity > 0)
            {
                float boiledWater = heatQuantity / waterL;
                boiledWater = (boiledWater > waterAmount) ? waterAmount : boiledWater;
                return new Water(waterAmount - boiledWater, temperature);
            } 
            else
            {
                temperature += heatQuantity * 1000 / (waterC * waterAmount);
                return this;
            }
        }
        // Получить температуру
        public float GetTemperature() => temperature;
    }
}