namespace MyElectricTeapot
{
    public class PowerButton
    {
        // Состояние кнопки
        public bool State { get; set; }

        // Переключить
        public void Toggle() => State = !State;
    }
}