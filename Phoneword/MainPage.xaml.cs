namespace Phoneword;
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    string translatedNumber;

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);
        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Llamar al " + translatedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Llamar al";
        }
    }
    async void OnCall(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlert(
            "Marca Telefono",
            "Seguro de llamar al " + translatedNumber + "?",
            "SI",
            "No"))
        {
            try
            {
                if (PhoneDialer.Default.IsSupported)
                    PhoneDialer.Default.Open(translatedNumber);
                else
                    await DisplayAlert("No soportado", "El marcado telefónico no es soportado en este dispositivo.", "OK");
            }
            catch (ArgumentNullException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid.", "OK");
            }
            catch (Exception)
            {
                // Other error has occurred.
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}