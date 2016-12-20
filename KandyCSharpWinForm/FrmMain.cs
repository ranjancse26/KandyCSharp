using System;
using System.Windows.Forms;
using KandyCSharp.Kandy;
using Newtonsoft.Json;

namespace KandyCSharpWinForm
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            var userAccessTokenService = new UserAccessTokenService();
            var userAccessToken = userAccessTokenService.GetUserAccessToken(txtUserID.Text.Trim());

            var deviceService = new DeviceService();
            var messages = deviceService.GetPendingMessages(userAccessToken, txtDeviceID.Text.Trim());
            rtbMessages.Text = JsonConvert.SerializeObject(messages);
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }
    }
}
