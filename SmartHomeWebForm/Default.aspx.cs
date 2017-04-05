using SmartHome;
using SmartHomeWebForm.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartHomeWebForm
{
    public partial class Default : System.Web.UI.Page
    {

        List<IDevicable> devices;
        Factory smartHomeFactory = new Factory(); // Создаем фабрику для создания устройств.


        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                devices = new List<IDevicable>();//Создаем пустой массив для устройств.
                devices.Add(smartHomeFactory.CreatorTV("Samsung"));
                devices.Add(smartHomeFactory.CreatorSound("Casio"));
                devices.Add(smartHomeFactory.CreatorConditioner("Panas"));
                devices.Add(smartHomeFactory.CreatorHeater("Hot Heater"));
                devices.Add(smartHomeFactory.CreatorBlower("Oriston"));
                Session["Devices"] = devices;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                devices = (List<IDevicable>)Session["Devices"];
                AddButtonBlower.Click += AddNewDeviceButtonClic;
                AddButtonConditioner.Click += AddNewDeviceButtonClic;
                AddButtonHeater.Click += AddNewDeviceButtonClic;
                AddButtonSD.Click += AddNewDeviceButtonClic;
                AddButtonTV.Click += AddNewDeviceButtonClic;
            }

            foreach (IDevicable dev in devices) //Создаем графику для устройств.
            {
                AddPanelDevice(dev);
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
        }

        protected void AddNewDeviceButtonClic(object sender, EventArgs e)
        {
            // обработчик на добавление устройств
            IDevicable newDevice;
            string deviceID = ((ImageButton)sender).ID;//определяем ID кнопки на которой сработало событие
            string nameDevice = TextBoxNameNewDevice.Text;//сохраняем имя нового устройства
            if (nameDevice.Length != 0)//проверяем пустая строка или нет
            {
                bool nameDuble = devices.Any(i => i.Name == nameDevice);//Ищем устройство с подобным именем
                if (!nameDuble)
                {
                    switch (deviceID)
                    {
                        case "AddButtonBlower":
                            newDevice = smartHomeFactory.CreatorBlower(nameDevice);
                            break;
                        case "AddButtonConditioner":
                            newDevice = smartHomeFactory.CreatorConditioner(nameDevice);
                            break;
                        case "AddButtonHeater":
                            newDevice = smartHomeFactory.CreatorHeater(nameDevice);
                            break;
                        case "AddButtonSD":
                            newDevice = smartHomeFactory.CreatorSound(nameDevice);
                            break;
                        default:
                            newDevice = smartHomeFactory.CreatorTV(nameDevice);
                            break;
                    }
                    devices.Add(newDevice);//добавляем в коллекцию новое устройство
                    AddPanelDevice(newDevice);//добавляем на экран новое устройство
                }
            }
        }
        protected void AddPanelDevice(IDevicable addDevice)
        {
            Panel1.Controls.Add(new RenderingDevices(addDevice, devices));
        }
    }
}