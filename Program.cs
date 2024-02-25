using System;
using System.Collections.Generic;
using System.Linq;
using static SolarPanel;


public interface IRotatable // Interfaz para rotar paneles solares
{
    void Rotation(int degrees); // Método para rotar paneles solares
}

public interface ISwitchable // Interfaz para encender y apagar elementos
{
    bool IsSwitchedOn { get; } // Solo lectura
    void SwitchOn(); // Encender
    void SwitchOff(); // Apagar
}
public abstract class Element 
{
    public Element(string name) // Constructor
    {
        Name = name;
    }
    public string Name { get; set; } // Propiedad

    public virtual string ToString() // Método ToString
    {
        return $"Name: {Name}";
    }

}

public class SolarPanel : Element, IRotatable
{
    public SolarPanel(string name) : base(name) // Constructor
    {
        Degrees = 0;
    }

    public int Degrees { get; private set; } // Propiedad

    public void Rotation(int degrees) // Método para rotar paneles solares
    {
        Degrees += degrees;

        if (Degrees > 90)
        {
            Degrees = 90;
        }
        else if (Degrees < -90)
        {
            Degrees = -90;
        }
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Degrees: {Degrees}";
    }

    public class RotationMotor : Element, ISwitchable
    {
        public bool IsSwitchedOn { get; private set; } // Propiedad
        public int Degrees { get; private set; } // Propiedad

        public RotationMotor(string name, bool initialSituation) : base(name) // Constructor
        {
            IsSwitchedOn = initialSituation; // Inicializar propiedad
            Degrees = 0;  // Inicializar propiedad
        }

        public void SwitchOn() // Método para encender motor de rotación
        {
            IsSwitchedOn = true; // Cambiar propiedad
        }

        public void SwitchOff() // Método para apagar motor de rotación
        {
            IsSwitchedOn = false; // Cambiar propiedad
            Console.WriteLine("Rotation motor is off"); // Mostrar mensaje
        }

        public void Rotate(int degrees) // Método para rotar motor de rotación
        {
            Degrees += degrees; // Cambiar propiedad

            // Validar rango de grados
            if (Degrees >= 360) 
            {
                Degrees -= 360;
            }
            else if (Degrees < 0)
            {
                Degrees += 360;
            }
        }

        public override string ToString() // Método ToString
        {
            return $"{base.ToString()}, IsSwitchedOn: {IsSwitchedOn}, Degrees: {Degrees}";
        }
    }

    public class Inverter : Element, ISwitchable
    {
        public bool IsSwitchedOn { get; private set; } // Propiedad

        public Inverter(string name, bool initialSituation) : base(name) // Constructor
        {
            IsSwitchedOn = initialSituation;
        }

        public void SwitchOn() 
        {
            IsSwitchedOn = true;
        }

        public void SwitchOff()
        {
            IsSwitchedOn = false;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, IsSwitchedOn: {IsSwitchedOn}";
        }
    }

    public class Battery : Element
    {
        public int Charge { get; private set; } // Propiedad
        private const int MaxCharge = 100; // Propiedad constante

        public Battery(string name) : base(name) // Constructor
        {
            Charge = 0; // Inicializar propiedad
        }

        public int Charging(int volts) // Método para cargar batería
        {
            Charge += volts; // Cambiar propiedad
             
            // Validar rango de carga
            if (Charge > MaxCharge)
            {
                Charge = MaxCharge;
                return -1;
            }

            return 0; // Retornar valor
        }

        public void Discharging() // Método para descargar batería
        {
            Charge = 0;
        }

        public override string ToString() // Método ToString
        {
            return $"{base.ToString()}, Charge: {Charge}";
        }
    }

    public class Sensor : SolarPanel
    {
        public string Type { get; set; } // Propiedad

        public Sensor(string name, string type) : base(name) // Constructor
        {
            Type = type; // Inicializar propiedad
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Type: {Type}";
        }
    }

}

class Program
{
    static void Main()
    {
        SolarPanel solarPanel1 = new SolarPanel("SolarPanel1"); // Crear un objeto de tipo SolarPanel
        SolarPanel solarPanel2 = new SolarPanel("SolarPanel2");
        SolarPanel solarPanel3 = new SolarPanel("SolarPanel3");

        RotationMotor rotationMotor1 = new RotationMotor("RotationMotor1", true); // Crear un objeto de tipo RotationMotor
        RotationMotor rotationMotor2 = new RotationMotor("RotationMotor2", true);
        RotationMotor rotationMotor3 = new RotationMotor("RotationMotor3", true);

        Inverter inverter = new Inverter("Inverter1", true); // Crear un objeto de tipo Inverter

        Battery battery1 = new Battery("Battery1"); // Crear un objeto de tipo Battery
        Battery battery2 = new Battery("Battery2");
        Battery battery3 = new Battery("Battery3");

        Sensor sensor1 = new Sensor("Sensor1", "Type1"); // Crear un objeto de tipo Sensor
        Sensor sensor2 = new Sensor("Sensor2", "Type2");
        Sensor sensor3 = new Sensor("Sensor3", "Type3");
        Sensor sensor4 = new Sensor("Sensor4", "Type4");
        Sensor sensor5 = new Sensor("Sensor5", "Type5");

        List<Element> elements = new List<Element>() // Crear una lista de elementos
        {
            solarPanel1, solarPanel2, solarPanel3,
            rotationMotor1, rotationMotor2, rotationMotor3,
            inverter,
            battery1, battery2, battery3,
            sensor1, sensor2, sensor3, sensor4, sensor5
        };

        foreach (var element in elements) // Recorrer la lista de elementos y acceder a sus métodos y propiedades (Casting)
        {
            if (element is SolarPanel solarPanel)
            {
                //Acceder a los métodos y propiedades de SolarPanel
                solarPanel.Rotation(45);
            }
            else if (element is RotationMotor rotationMotor)
            {
                //Acceder a los métodos y propiedades de RotationMotor
                rotationMotor.SwitchOn();
            }
            else if (element is Inverter inverterElement)
            {
                //Acceder a los métodos y propiedades de Inverter
                inverterElement.SwitchOn();
            }
            else if (element is Battery battery)
            {
                //Acceder a los métodos y propiedades de Battery
                battery.Charging(50);
            }
            else if (element is Sensor sensor)
            {
                //Acceder a los métodos y propiedades de Sensor
                sensor.Type = "Type6";
            }
        }

        while (true)
        {
            Console.WriteLine("***************************************");
            Console.WriteLine("            MENÚ             ");
            Console.WriteLine("***************************************");
            Console.WriteLine("1. Show elements");
            Console.WriteLine("2. Show switched on");
            Console.WriteLine("3. Check sensors");
            Console.WriteLine("4. Rotate panels");
            Console.WriteLine("5. Rotate motors");
            Console.WriteLine("6. Start production");
            Console.WriteLine("7. Stop production");
            Console.WriteLine("8. Discharge batteries");
            Console.WriteLine("9. Exit");

            string input = Console.ReadLine();
            if (int.TryParse(input, out int option))
            {
                switch (option)
                {
                    case 1: // Mostrar elementos
                        ShowElements(elements.ToArray());
                        break;
                    case 2: // Mostrar elementos encendidos
                        ShowSwitchedOn(elements.ToArray());
                        break;
                    case 3: // Mostrar sensores
                        CheckSensors(elements.ToArray());
                        break;
                    case 4: // Rotar paneles
                        RotatePanels(elements.ToArray());
                        break;
                    case 5: // Rotar motores
                        RotateMotors(elements.ToArray());
                        break;
                    case 6: // Iniciar producción
                        StartProduction(elements.ToArray());
                        break;
                    case 7: // Detener producción
                        StopProduction(elements.ToArray());
                        break;
                    case 8: // Descargar baterías
                        DischargeBatteries(elements.ToArray());
                        break;
                    case 9:
                        Console.WriteLine("Exit...");
                        return;
                }
            }
        }
    }

    public static void ShowElements(Element[] elements)
    {
        elements.ToList().ForEach(element => Console.WriteLine(element.ToString())); // Recorrer la lista y mostrar los elementos
    }

    public static void ShowSwitchedOn(Element[] elements)
    {
        elements.OfType<ISwitchable>() // Filtrar elementos de tipo ISwitchable
                .Where(switchable => switchable.IsSwitchedOn) // Filtrar elementos encendidos
                .ToList()   // Convertir a lista
                .ForEach(switchable => Console.WriteLine(switchable.ToString())); // Recorrer la lista y mostrar los elementos
    }


    public static void CheckSensors(Element[] elements) 
    {
        elements.OfType<Sensor>() // Filtrar elementos de tipo Sensor
                .ToList() // Convertir a lista
                .ForEach(sensor => Console.WriteLine(sensor.ToString())); // Recorrer la lista y mostrar los elementos
    }

    public static void RotatePanels(Element[] elements) 
    {
        elements.OfType<SolarPanel>() // Filtrar elementos de tipo SolarPanel
                .ToList() // Convertir a lista
                .ForEach(solarPanel => solarPanel.Rotation(45)); // Recorrer la lista y rotar los paneles
    }

    public static void RotateMotors(Element[] elements)
    {
        elements.OfType<RotationMotor>() // Filtrar elementos de tipo RotationMotor
                .ToList() // Convertir a lista
                .ForEach(rotationMotor => rotationMotor.Rotate(45)); // Recorrer la lista y rotar los motores
    }

    public static void StartProduction(Element[] elements)
    {
        elements.OfType<ISwitchable>() // Filtrar elementos de tipo ISwitchable
                .ToList() // Convertir a lista
                .ForEach(switchable => switchable.SwitchOn()); // Recorrer la lista y encender los elementos
    }

    public static void StopProduction(Element[] elements)
    {
        elements.OfType<ISwitchable>() // Filtrar elementos de tipo ISwitchable
                .ToList() // Convertir a lista
                .ForEach(switchable => switchable.SwitchOff());  // Recorrer la lista y apagar los elementos
    }

    public static void DischargeBatteries(Element[] elements)
    {
        elements.OfType<Battery>() // Filtrar elementos de tipo Battery
                .ToList() // Convertir a lista
                .ForEach(battery => battery.Discharging()); // Recorrer la lista y descargar las baterías
    }

}





