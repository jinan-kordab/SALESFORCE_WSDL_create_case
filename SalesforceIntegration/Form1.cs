using SalesforceIntegration.SFDC;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SalesforceIntegration
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		public object SforceService { get; private set; }

		private void Form1_Load(object sender, EventArgs e)
		{

			//developper acocunt
			//user: josiec@isd.ca
			//pass: chatterbox1

			string userName = "sendtestsms@gmail.com";
			string password = "luntikpogodi001";
			string securityToken = "9LceqWzqIlPWFfCEFuXt6AhW5";

			SforceService sfdcBinding = null;
			LoginResult currentLoginResult = null;
			sfdcBinding = new SforceService();
			try
			{
				currentLoginResult = sfdcBinding.login(userName, password + securityToken);
			}
			catch (System.Web.Services.Protocols.SoapException ex)
			{
				// This is likley to be caused by bad username or password
				sfdcBinding = null;
				throw (ex);
			}
			catch (Exception ex)
			{
				// This is something else, probably comminication
				sfdcBinding = null;
				throw (ex);
			}


			//Change the binding to the new endpoint
			sfdcBinding.Url = currentLoginResult.serverUrl;

			//Create a new session header object and set the session id to that returned by the login
			sfdcBinding.SessionHeaderValue = new SessionHeader();
			sfdcBinding.SessionHeaderValue.sessionId = currentLoginResult.sessionId;


			//Create a case

			Case myNewCase = new Case();

			//The status of the case, such as “New,” “Closed,” or “Escalated.” This field directly controls the IsClosed flag. Each predefined Status value implies an IsClosed flag value. For more information, see CaseStatus.
			myNewCase.Status = "New";

			//The reason why the case was created, such as Instructions not clear, or User didn’t attend training.
			myNewCase.Reason = "TESTING CASE CREATION";

			//The subject of the case. Limit: 255 characters
			myNewCase.Subject = "TEST CASE";

			//The name that was entered when the case was created. This field can't be updated after the case has been created. Label is Name.
			myNewCase.SuppliedName = "TEST CASE";

			//High, Medium, or Low
			myNewCase.Priority = "High";

			
		
			SaveResult[] createResults = sfdcBinding.create(new sObject[] { myNewCase });

			if (createResults[0].success)
			{
				string id = createResults[0].id;
				
			}
			else
			{
				string result = createResults[0].errors[0].message;
				
			}

			
			
		}
	}
}
