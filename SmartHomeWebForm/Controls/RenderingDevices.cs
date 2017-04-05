using SmartHome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SmartHomeWebForm.Controls
{
    public class RenderingDevices : PlaceHolder, IRenderingDevices
    {
        IDevicable device;
        List<IDevicable> devices;
        TextBox texBoxVolumeLevel;
        TextBox texBoxChanel;
        TextBox texBoxTemper;
        TextBox texBoxBass;
        Label labelMessageStateDevice;
        Image imagDevice;
        ImageButton buttonOnOff;
        ImageButton buttonChanelPrev;
        ImageButton buttonChanelNext;

        public RenderingDevices(IDevicable device, List<IDevicable> devices)
        {
            this.device = device;
            this.devices = devices;
            SectionDevice();
        }
        protected HtmlGenericControl Span(string innerHtml)//создание элемента span
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHtml;
            return span;
        }

        protected ImageButton CreateImageButt(string id, string cssClass, string imageUrl, string toolTip, ImageClickEventHandler eventClick)
        {
            ImageButton creatButt = new ImageButton();
            creatButt.ID = id + device.Name;
            creatButt.CssClass = cssClass;
            creatButt.ImageUrl = imageUrl;
            creatButt.ToolTip = toolTip;
            creatButt.Click += eventClick;
            return creatButt;
        }

        protected Label CreateLabel(string id, string cssClass)
        {
            Label createLabel = new Label();
            createLabel.ID = id + device.Name;
            createLabel.CssClass = cssClass;
            return createLabel;
        }
        protected TextBox CreateTextBox(string id, int text, string cssClass, string toolTip, EventHandler eventText)
        {
            TextBox createTexBox = new TextBox();
            createTexBox.ID = id + device.Name;
            createTexBox.CssClass = cssClass;
            createTexBox.ToolTip = toolTip;
            createTexBox.TextChanged += eventText;
            createTexBox.Text = Convert.ToString(text);
            return createTexBox;
        }
        //рисование графики устройств
        public void SectionDevice()//создание секции навигации для устройства
        {

            Panel panelDevice = new Panel();
            panelDevice.ID = "Panel" + device.Name;
            panelDevice.CssClass = "col-xs-4 devicePadding deviceBorder devicePanel";
            Controls.Add(panelDevice);

            //выводим имя устройства
            Label nameDevLebel = CreateLabel("Label", "col-xs-10 devicePadding deviceName");
            nameDevLebel.Text = device.Name;
            panelDevice.Controls.Add(nameDevLebel);
            //создаем кнопку удаления
            ImageButton buttonTrash = CreateImageButt("buttonTrash", "btn-md btn-danger imageButtonIcon",
                "content\\button-trash.png", "Удалить устройство", ButtonTrash_Click);
            panelDevice.Controls.Add(buttonTrash);

            imagDevice = new Image(); // Создаем изображение устройства
            imagDevice.CssClass = "col-xs-6 devicePadding imageIconDevice";//col-xs-4
            imagDevice.ID = "iconDevice" + device.Name;
            imagDevice.ToolTip = device.Name + " выкл.";
            imagDevice.ImageUrl = IconDevice();
            panelDevice.Controls.Add(imagDevice);

            //div в котором будет навигация устройства
            Panel panelNavDevice = new Panel();
            panelNavDevice.CssClass = "";
            panelNavDevice.ID = "PanelNavigation" + device.Name;
            panelDevice.Controls.Add(panelNavDevice);

            // в panelNavDevice добавляем элементы управления
            buttonOnOff = CreateImageButt("OnOff", "btn-md btn-danger imageButtonIcon",
                "content\\button-OnOff.png", "Включить устройство", ButtonOnOff_Click);
            panelNavDevice.Controls.Add(buttonOnOff);

            labelMessageStateDevice = CreateLabel("labelState", "text-error");
            panelNavDevice.Controls.Add(labelMessageStateDevice);

            panelNavDevice.Controls.Add(Span("<br />"));

            if (device is IVolumenable)
            {
                byte levelVolume = ((IVolumenable)device).Volume;
                texBoxVolumeLevel = CreateTextBox("texBoxVolumeLevel", levelVolume, 
                    "col-xs-2 devicePadding", "Установить громкость", texBox_TextChanged);
                panelNavDevice.Controls.Add(texBoxVolumeLevel);

                ImageButton buttonVolumeDown = CreateImageButt("buttonVolumeDown", "btn-md btn-success imageButtonIcon",
                    "content\\button-volumeDown.png", "Уменьшить громкость", Button_Click);
                panelNavDevice.Controls.Add(buttonVolumeDown);

                ImageButton buttonVolumeUp = CreateImageButt("buttonVolumeUp", "btn-md btn-success imageButtonIcon",
                    "content\\button-volumeUp.png", "Увеличить громкость", Button_Click);
                panelNavDevice.Controls.Add(buttonVolumeUp);

                ImageButton buttonVolumeMute = CreateImageButt("buttonVolumeMute", "btn-md btn-success imageButtonIcon",
                    "content\\button-volumeMute.png", "Отключить громкость", Button_Click);
                panelNavDevice.Controls.Add(buttonVolumeMute);

                panelNavDevice.Controls.Add(Span("<br />"));
            }
            if (device is ISwitchable)
            {
                int chanel = ((ISwitchable)device).Current;
                texBoxChanel = CreateTextBox("textBoxChanel", chanel, 
                    "col-xs-2 devicePadding", "Установить канал", texBox_TextChanged);
                panelNavDevice.Controls.Add(texBoxChanel);

                buttonChanelPrev = CreateImageButt("buttonChanelPrev", "btn-md btn-success imageButtonIcon",
                    "content\\button-previos.png", "Предыдущий канал", Button_Click);
                panelNavDevice.Controls.Add(buttonChanelPrev);

                buttonChanelNext = CreateImageButt("buttonChanelNext", "btn-md btn-success imageButtonIcon",
                    "content\\button-next.png", "Следующий канал", Button_Click);
                if (device is IVolumenable && device is ISwitchable && device is IBassable)
                {
                    texBoxChanel.ToolTip = "Установить трек";
                    buttonChanelPrev.ToolTip = "Предыдущий трек";
                    buttonChanelNext.ToolTip = "Следующий трек";
                }
                panelNavDevice.Controls.Add(buttonChanelNext);

                panelNavDevice.Controls.Add(Span("<br />"));
            }
            if (device is ITemperaturable)
            {
                byte levelTemper = ((ITemperaturable)device).Temperature;
                texBoxTemper = CreateTextBox("texBoxTemper", levelTemper, 
                    "col-xs-2 devicePadding", "Установить температуру", texBox_TextChanged);
                panelNavDevice.Controls.Add(texBoxTemper);

                ImageButton buttonTemperDown = CreateImageButt("buttonTemperDown", "btn-md btn-success imageButtonIcon",
                    "content\\button-down.png", "Уменьшить температуру", Button_Click);
                panelNavDevice.Controls.Add(buttonTemperDown);

                ImageButton buttonTemperUp = CreateImageButt("buttonTemperUp", "btn-md btn-success imageButtonIcon",
                    "content\\button-up.png", "Увеличить температуру", Button_Click);
                panelNavDevice.Controls.Add(buttonTemperUp);

                panelNavDevice.Controls.Add(Span("<br />"));
            }
            if (device is IBassable)
            {
                byte levelBass = ((IBassable)device).BassLevel;
                texBoxBass = CreateTextBox("texBoxBass", levelBass, "col-xs-2 devicePadding", 
                    "Установить уровень бассов", texBox_TextChanged);
                panelNavDevice.Controls.Add(texBoxBass);

                ImageButton buttonBassDown = CreateImageButt("buttonBassDown", "btn-md btn-success imageButtonIcon",
                    "content\\button-down.png", "Уменьшить уровень бассов", Button_Click);
                panelNavDevice.Controls.Add(buttonBassDown);

                ImageButton buttonBassUp = CreateImageButt("buttonBassUp", "btn-md btn-success imageButtonIcon",
                    "content\\button-up.png", "Увеличить уровень бассов", Button_Click);
                panelNavDevice.Controls.Add(buttonBassUp);

                panelNavDevice.Controls.Add(Span("<br />"));
            }

            if (device is ISpeedAirable)
            {
                ImageButton buttonSpeeAirLow = CreateImageButt("buttonSpeeAirLow", "btn-md btn-success imageButtonIcon",
                    "content\\button-low.png", "Низкая интенсивность", Button_Click);
                panelNavDevice.Controls.Add(buttonSpeeAirLow);

                ImageButton buttonSpeeAirMed = CreateImageButt("buttonSpeeAirMed", "btn-md btn-success imageButtonIcon",
                    "content\\button-medium.png", "Средняя интенсивность", Button_Click);
                panelNavDevice.Controls.Add(buttonSpeeAirMed);

                ImageButton buttonSpeeAirHight = CreateImageButt("buttonSpeeAirHight", "btn-md btn-success imageButtonIcon",
                    "content\\button-hight.png", "Высокая интенсивность", Button_Click);
                panelNavDevice.Controls.Add(buttonSpeeAirHight);
            }
        }

        void texBox_TextChanged(object sender, EventArgs e)
        {
            string textTextBox = ((TextBox)sender).Text;
            int levelValue;
            bool intParse = int.TryParse(textTextBox, out levelValue);
            byte levelValueByte;
            bool byteParse = byte.TryParse(textTextBox, out levelValueByte);

            if (intParse && device is ISwitchable && ((TextBox)sender).ID == "textBoxChanel" + device.Name)
            {
                ((ISwitchable)device).Current = levelValue;
                texBoxChanel.Text = Convert.ToString(((ISwitchable)device).Current);
            }
            else if (byteParse && device is IVolumenable && ((TextBox)sender).ID == "texBoxVolumeLevel" + device.Name)
            {
                ((IVolumenable)device).Volume = levelValueByte;
                texBoxVolumeLevel.Text = Convert.ToString(((IVolumenable)device).Volume);
            }
            else if (byteParse && device is ITemperaturable && ((TextBox)sender).ID == "texBoxTemper" + device.Name)
            {
                ((ITemperaturable)device).Temperature = levelValueByte;
                texBoxTemper.Text = Convert.ToString(((ITemperaturable)device).Temperature);
            }
            else if (byteParse && device is IBassable && ((TextBox)sender).ID == "texBoxBass" + device.Name)
            {
                ((IBassable)device).BassLevel = levelValueByte;
                texBoxBass.Text = Convert.ToString(((IBassable)device).BassLevel);
            }
        }

        void Button_Click(object sender, ImageClickEventArgs e) // обработка кнопки управления звуком
        {
            if (device.State)
            {
                if (((ImageButton)sender).ID == "buttonVolumeDown" + device.Name)
                {
                    ((IVolumenable)device).VolumeDown();
                    byte levelVolume = ((IVolumenable)device).Volume;
                    texBoxVolumeLevel.Text = Convert.ToString(levelVolume);
                }
                else if (((ImageButton)sender).ID == "buttonVolumeUp" + device.Name)
                {
                    ((IVolumenable)device).VolumeUp();
                    byte levelVolume = ((IVolumenable)device).Volume;
                    texBoxVolumeLevel.Text = Convert.ToString(levelVolume);
                }
                else if (((ImageButton)sender).ID == "buttonVolumeMute" + device.Name)
                {
                    ((IVolumenable)device).Volume = 0;
                    byte levelVolume = ((IVolumenable)device).Volume;
                    texBoxVolumeLevel.Text = Convert.ToString(levelVolume);
                }

                if (((ImageButton)sender).ID == "buttonChanelPrev" + device.Name)
                {
                    ((ISwitchable)device).Previous();
                    int chanelCurrent = ((ISwitchable)device).Current;
                    texBoxChanel.Text = Convert.ToString(chanelCurrent);
                    imagDevice.ImageUrl = IconDevice();
                }
                else if (((ImageButton)sender).ID == "buttonChanelNext" + device.Name)
                {
                    ((ISwitchable)device).Next();
                    int chanelCurrent = ((ISwitchable)device).Current;
                    texBoxChanel.Text = Convert.ToString(chanelCurrent);
                    imagDevice.ImageUrl = IconDevice();
                }

                if (((ImageButton)sender).ID == "buttonTemperDown" + device.Name)
                {
                    ((ITemperaturable)device).TemperatureDown();
                    byte temperature = ((ITemperaturable)device).Temperature;
                    texBoxTemper.Text = Convert.ToString(temperature);
                }
                else if (((ImageButton)sender).ID == "buttonTemperUp" + device.Name)
                {
                    ((ITemperaturable)device).TemperatureUp();
                    byte temperature = ((ITemperaturable)device).Temperature;
                    texBoxTemper.Text = Convert.ToString(temperature);
                }

                if (((ImageButton)sender).ID == "buttonBassDown" + device.Name)
                {
                    ((IBassable)device).BassDown();
                    byte levelBass = ((IBassable)device).BassLevel;
                    texBoxBass.Text = Convert.ToString(levelBass);
                }
                else if (((ImageButton)sender).ID == "buttonBassUp" + device.Name)
                {
                    ((IBassable)device).BassUp();
                    byte levelBass = ((IBassable)device).BassLevel;
                    texBoxBass.Text = Convert.ToString(levelBass);
                }

                if (((ImageButton)sender).ID == "buttonSpeeAirLow" + device.Name)
                {
                    ((ISpeedAirable)device).SpeedAirLow();
                    imagDevice.ImageUrl = IconDevice();
                }
                else if (((ImageButton)sender).ID == "buttonSpeeAirMed" + device.Name)
                {
                    ((ISpeedAirable)device).SpeedAirMedium();
                    imagDevice.ImageUrl = IconDevice();
                }
                else if (((ImageButton)sender).ID == "buttonSpeeAirHight" + device.Name)
                {
                    ((ISpeedAirable)device).SpeedAirHight();
                    imagDevice.ImageUrl = IconDevice();
                }
            }
            else
            {
                labelMessageStateDevice.Text = "Устройство выкл.";
            }
        }

        void ButtonOnOff_Click(object sender, ImageClickEventArgs e)
        {
            if (device.State)
            {
                device.Off();
                imagDevice.ToolTip = device.Name + "выкл.";
                buttonOnOff.CssClass = "btn-md btn-danger imageButtonIcon";
                buttonOnOff.ToolTip = "Включить устройство";
                imagDevice.ImageUrl = IconDevice();
            }
            else
            {
                device.On();
                imagDevice.ToolTip = device.Name + " вкл.";
                buttonOnOff.CssClass = "btn-md btn-success imageButtonIcon";
                buttonOnOff.ToolTip = "Выключить устройство";
                imagDevice.ImageUrl = IconDevice();
                labelMessageStateDevice.Text = "";
            }
        }

        private void ButtonTrash_Click(object sender, ImageClickEventArgs e)
        {
            devices.Remove(device);
            Parent.Controls.Remove(this);
        }

        private string IconDevice()
        {
            string urlImage = "";
            if (device is IVolumenable && device is ISwitchable && device is IColorRedable)
            {
                if (device.State && ((ISwitchable)device).Current == 1)
                {
                    urlImage = "content\\TVCats.gif";
                }
                else if (device.State && ((ISwitchable)device).Current == 2)
                {
                    urlImage = "content\\TVDogs.gif";
                }
                else if (device.State)
                {
                    urlImage = "content\\TVMontains.gif";
                }
                else
                {
                    urlImage = "content\\tvOff.png";
                }
            }
            else if (device is IVolumenable && device is ISwitchable && device is IBassable)
            {
                if (device.State)
                {
                    urlImage = "content\\SoundDeviceOn.gif";
                }
                else
                {
                    urlImage = "content\\SoundDeviceOff.png";
                }
            }
            else if (device is ITemperaturable && device is ISpeedAirable)
            {
                if (device.State && ((ISpeedAirable)device).LevelSpeed == Speed.Low)
                {
                    urlImage = "content\\conditionerOnLow.gif";
                }
                else if (device.State && ((ISpeedAirable)device).LevelSpeed == Speed.Medium)
                {
                    urlImage = "content\\conditionerOnMedium.gif";
                }
                else if (device.State && ((ISpeedAirable)device).LevelSpeed == Speed.Hight)
                {
                    urlImage = "content\\conditionerOnHight.gif";
                }
                else
                {
                    urlImage = "content\\conditionerOff.png";
                }
            }
            else if (device is ITemperaturable)
            {
                if (device.State)
                {
                    urlImage = "content\\heaterOn.gif";
                }
                else
                {
                    urlImage = "content\\heaterOff.png";
                }
            }
            else if (device is ISpeedAirable)
            {
                if (device.State && ((ISpeedAirable)device).LevelSpeed == Speed.Low)
                {
                    urlImage = "content\\blowerOnLow.gif";
                }
                else if (device.State && ((ISpeedAirable)device).LevelSpeed == Speed.Medium)
                {
                    urlImage = "content\\blowerOnMedium.gif";
                }
                else if (device.State && ((ISpeedAirable)device).LevelSpeed == Speed.Hight)
                {
                    urlImage = "content\\blowerOnHight.gif";
                }
                else
                {
                    urlImage = "content\\blowerOff.png";
                }
            }
            return urlImage;
        }
    }
}