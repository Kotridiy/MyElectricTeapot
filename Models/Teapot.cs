using System;

namespace MyElectricTeapot
{
    public class Teapot
    {
        // внешняя температура
        public const float OUTSIDE_TEMPERATURE = 22;
        // Контейнер для воды
        protected WaterContainer WaterContainer { get; private set; }
        // Нагревательный элемент
        protected Heater Heater { get; private set; }
        // Кнопка включения
        protected PowerButton PowerButton { get; private set; }

        public Teapot(WaterContainer waterContainer, Heater heater, PowerButton powerButton)
        {
            WaterContainer = waterContainer ?? throw new ArgumentNullException(nameof(waterContainer));
            Heater = heater ?? throw new ArgumentNullException(nameof(heater));
            PowerButton = powerButton ?? throw new ArgumentNullException(nameof(powerButton));
        }

        // Возвращает количество воды (мл) в чайнике
        public float GetWaterAmount() => WaterContainer.InnerWater.waterAmount;
        // Возвращает температуру воды в чайнике
        public float GetWaterTemperature() => WaterContainer.InnerWater.GetTemperature();

        // Обновить состояние чайника
        public void Update(float timedelta = 1)
        {
            float heatQuantity = 0;
            if (PowerButton.State)
            {
                heatQuantity = Heater.Work(timedelta);
            }
            WaterContainer.Update(timedelta, heatQuantity);
        }

        // Включить чайник
        public void PowerOn() => PowerButton.State = true;
        // Выключить чайник
        public void PowerOff() => PowerButton.State = false;

        // Добавить воды в чайник
        public void AddWater(Water water) => WaterContainer.AddWater(water);

        // Вылить всю воду из чайника
        public Water PourWater() => WaterContainer.PourWater();
        // Вылить количество воды (мл) из чайника
        public Water PourWater(int amount) => WaterContainer.PourWater(amount);

        public override string ToString()
        {
            string stateString = "";
            stateString += PowerButton.State ? "включён, " : "выключен, ";
            stateString += String.Format("{0:f0}", GetWaterAmount()) + "мл, ";
            stateString += String.Format("{0:f1}", GetWaterTemperature()) + " C";

            return stateString;
        }
    }
}
