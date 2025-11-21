using ProgramPartListWeb.Areas.PC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProgramPartListWeb.Areas.PC.Repository
{
    public class PatrolEmailService
    {

        public static string ReturnEmailBody(string recipientName,
           string reg,
           string departmentName,
           string approvedBy,
           string link,
           string additionalNotes = "",
           string emailType = "default")
        {
            return "";
        }



        // Sending Success Approval Email
        public static string ApprovalConfirmedEmailBody(
           string recipientName,
           string reg,
           string departmentName,
           string approvedBy,
           string link,
           string additionalNotes = "",
           string emailType = "default")
        {
            string messageContent = "";
            string headerColor = "linear-gradient(135deg, #28a745, #20c997)";
            string buttonColor = "#28a745";


            switch (emailType.ToLower())
            {
                case "inspestorapproved":
                    messageContent = $@"
                            <p>This is to inform you that your Countermeasure has been <strong>successfully approved</strong>.</p>

                            <table class='details-table'>
                                <tr>
                                    <td class='label'>Registration No:</td>
                                    <td><strong>{reg}</strong></td>
                                </tr>
                                <tr>
                                    <td class='label'>Status:</td>
                                    <td><span style='color: #28a745; font-weight: bold;'>✅ APPROVED</span></td>
                                </tr>
                                <tr>
                                    <td class='label'>Department:</td>
                                    <td>{departmentName}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Approved By:</td>
                                    <td>{approvedBy}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Approval Date:</td>
                                    <td>{DateTime.Now.ToString("MMMM dd, yyyy")}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Approval Time:</td>
                                    <td>{DateTime.Now.ToString("hh:mm tt")}</td>
                                </tr>
                                <tr>
                                    <td class='label'>View Details:</td>
                                    <td><a href='{link}' class='button-approved'>View Approved Report</a></td>
                                </tr>
                            </table>

                            <div class='success-box'>
                                <p><strong>✅ Approval Completed Successfully</strong></p>
                                <p>The registration process has been completed and the report is now officially approved. This case is now closed.</p>
                                {(string.IsNullOrEmpty(additionalNotes) ? "" : $@"<p><strong>Additional Notes:</strong> {additionalNotes}</p>")}
                            </div>";
                    break;
                case "ManagersApproved":
                    messageContent = $@"
                            <p>This is to inform you that the registration has been <strong>successfully approved</strong>.</p>

                            <table class='details-table'>
                                <tr>
                                    <td class='label'>Registration No:</td>
                                    <td><strong>{reg}</strong></td>
                                </tr>
                                <tr>
                                    <td class='label'>Status:</td>
                                    <td><span style='color: #28a745; font-weight: bold;'>✅ APPROVED</span></td>
                                </tr>
                                <tr>
                                    <td class='label'>Department:</td>
                                    <td>{departmentName}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Approved By:</td>
                                    <td>{approvedBy}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Approval Date:</td>
                                    <td>{DateTime.Now.ToString("MMMM dd, yyyy")}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Approval Time:</td>
                                    <td>{DateTime.Now.ToString("hh:mm tt")}</td>
                                </tr>
                                <tr>
                                    <td class='label'>View Details:</td>
                                    <td><a href='{link}' class='button-approved'>View Approved Report</a></td>
                                </tr>
                            </table>

                            <div class='success-box'>
                                <p><strong>✅ Approval Completed Successfully</strong></p>
                                <p>The registration process has been completed and the report is now officially approved. This case is now closed.</p>
                                {(string.IsNullOrEmpty(additionalNotes) ? "" : $@"<p><strong>Additional Notes:</strong> {additionalNotes}</p>")}
                            </div>";
                    break;
            }

            string emailBody = $@"
        <html>
        <head>
            <style>
                body {{ font-family: 'Segoe UI', Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; background-color: #f5f5f5; }}
                .container {{ max-width: 700px; margin: 0 auto; padding: 0; background: white; }}
                .header {{ background: {headerColor}; color: white; padding: 20px; }}
                .header h2 {{ margin: 0; font-size: 18px; font-weight: bold; }}
                .email-info {{ background: #e9ecef; padding: 8px 20px; font-size: 11px; color: #666; border-bottom: 1px solid #ddd; }}
                .content {{ padding: 25px !important; }}
                .button-approved {{ 
                    display: inline-block; 
                    padding: 12px 24px; 
                    background-color: {buttonColor}; 
                    color: white; 
                    text-decoration: none; 
                    border-radius: 4px; 
                    font-weight: bold; 
                    margin: 5px 0;
                }}
                .footer {{ margin-top: 25px; padding-top: 20px; border-top: 1px solid #e9ecef; font-size: 11px; color: #6c757d; }}
                .details-table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
                .details-table td {{ padding: 12px; border: 1px solid #dee2e6; vertical-align: top; }}
                .details-table tr:nth-child(even) {{ background-color: #f8f9fa; }}
                .label {{ font-weight: bold; color: #495057; width: 30%; background-color: #f8f9fa; }}
                .system-links {{ background: #e7f3ff; padding: 15px; border-radius: 4px; margin: 20px 0; }}
                .success-box {{ 
                    background: #d4edda; 
                    border: 1px solid #c3e6cb; 
                    border-radius: 4px; 
                    padding: 20px; 
                    margin: 20px 0; 
                    color: #155724;
                }}
                .success-box strong {{ color: #0f5132; }}
                .browser-notice {{ font-size: 11px; color: #666; text-align: center; margin-bottom: 20px; }}
                .completion-badge {{
                    background: #28a745;
                    color: white;
                    padding: 8px 15px;
                    border-radius: 20px;
                    font-size: 14px;
                    font-weight: bold;
                    display: inline-block;
                    margin: 10px 0;
                }}
            </style>
        </head>
        <body>
            <div class='container'>
                

                <div class='content'>
                    <p>Hi {recipientName},</p>
                    <p>Good day!</p>
        
                    {messageContent}

                    <div class='completion-badge'>
                        ✓ PROCESS COMPLETED
                    </div>

                    <div class='system-links'>
                        <p><strong>Access the approved report:</strong></p>
                        <p><a href='{link}' class='button-approved'>View Approved Report Details</a></p>
                    </div>

                    <div class='note-box'>
                        <p><strong>Note:</strong> This approval is final and the case is now closed in the system.</p>
                    </div>
                </div>
    
                <div class='footer'>
                    <p>This is an automated confirmation message. Please do not reply to this email.</p>
                    <p>&copy; {DateTime.Now.Year} Sanyo Denki Phil. All rights reserved.</p>
                </div>
            </div>
        </body>
        </html>";

            return emailBody;
        }


        public static string RegistrationEmailBodyV2(
            string fullName, 
            string reg, 
            string DepartmentName, 
            string link, 
            string emailType = "default")
        {
            string messageContent = "";
            string subjectLine = "";
            string buttonText = "";
            string headerColor = "";


            switch (emailType.ToLower())
            {
                case "processowner":
                    messageContent = $@"
                            <p>This is to inform you that you have a response and are <strong>required to submit a countermeasure</strong> for the Patrol Inspection report.</p>
                
                            <table class='details-table'>
                                <tr>
                                    <td class='label'>Registration No:</td>
                                    <td><strong>{reg}</strong></td>
                                </tr>
                               
                                <tr>
                                    <td class='label'>Dept. /Section:</td>
                                    <td>{DepartmentName}</td>
                                </tr>
                                <tr>
                                    <td class='label'>Link of the Registration No. :</td>
                                    <td> <a href='{link}' class='button'> Patrol inspection report </a></td>
                                </tr>
                            </table>";
                    subjectLine = "[FOLLOW UP - REGISTRATION REPORT] 'For Review/Submit CounterMeasure'";
                    buttonText = "Submit Countermeasure";
                    headerColor = "linear-gradient(135deg, #dc3545, #c82333)";
                    break;
                case "inpectorapproval":
                    messageContent = $@"
                            <p>This is to inform you that you have a <strong>For Review/Approval</strong> Patrol Inspection report.</p>
                
                            <table class='details-table'>
                                <tr>
                                    <td class='label'>Registration  No:</td>
                                    <td><strong>{reg}</strong></td>
                                </tr>
                                <tr>
                                    <td class='label'>Person Incharge :</td>
                                    <td><strong></strong></td>
                                </tr>
                                  <tr>
                                    <td class='label'>Dept. /Section:</td>
                                    <td><strong>{DepartmentName}</strong></td>
                                </tr>
                                <tr>
                                    <td class='label'>Link of the Registration No. :</td>
                                    <td> <a href='{link}' class='button'> Patrol inspection report </a></td>
                                </tr>
                            </table>";
                    subjectLine = "[FOLLOW UP - NONCON REPORT] 'For Review/Approval'";
                    buttonText = "For Review Registration";
                    headerColor = "linear-gradient(135deg, #dc3545, #c82333)";
                    break;


                default:
                    messageContent = $@"
                            <p>This is to inform you that you have a <strong>Registration Verification</strong> report.</p>
                
                            <table class='details-table'>
                                <tr>
                                    <td class='label'>Registration:</td>
                                    <td><strong>{reg}</strong></td>
                                </tr>
                                <tr>
                                    <td class='label'>Details:</td>
                                    <td>
                                        <strong>Next Steps:</strong><br>
                                        • Click the verification link below<br>
                                        • Complete any remaining required information<br>
                                        • Submit your registration for processing
                                    </td>
                                </tr>
                                <tr>
                                    <td class='label'>Issuing Department:</td>
                                    <td>Registration System</td>
                                </tr>
                            </table>";
                    subjectLine = "[REGISTRATION VERIFICATION] 'Action Required'";
                    buttonText = "Complete Registration";
                    headerColor = "linear-gradient(135deg, #007bff, #0056b3)";
                    break;
            }

            string emailBody = $@"
                    <html>
                    <head>
                        <style>
                            body {{ font-family: 'Segoe UI', Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; background-color: #f5f5f5; }}
                            .container {{ max-width: 700px; margin: 0 auto; padding: 0; background: white; }}
                            .header {{ background: {headerColor}; color: white; padding: 20px; }}
                            .header h2 {{ margin: 0; font-size: 18px; font-weight: bold; }}
                            .email-info {{ background: #e9ecef; padding: 8px 20px; font-size: 11px; color: #666; border-bottom: 1px solid #ddd; }}
                            .content {{ padding: 25px !important; }}
                            .button {{ display: inline-block; padding: 12px 24px; background-color: #dc3545; color: white; text-decoration: none; border-radius: 4px; font-weight: bold; }}
                            .footer {{ margin-top: 25px; padding-top: 20px; border-top: 1px solid #e9ecef; font-size: 11px; color: #6c757d; }}
                            .details-table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
                            .details-table td {{ padding: 12px; border: 1px solid #dee2e6; vertical-align: top; }}
                            .details-table tr:nth-child(even) {{ background-color: #f8f9fa; }}
                            .label {{ font-weight: bold; color: #495057; width: 25%; background-color: #f8f9fa; }}
                            .system-links {{ background: #e7f3ff; padding: 15px; border-radius: 4px; margin: 20px 0; }}
                            .note-box {{ background: #fff3cd; border: 1px solid #ffeaa7; border-radius: 4px; padding: 12px; margin: 15px 0; font-size: 12px; }}
                            .browser-notice {{ font-size: 11px; color: #666; text-align: center; margin-bottom: 20px; }}
                        </style>
                    </head>
                    <body>
                        <div class='container'>

                            <div class='content'>
                                <p>Hi {fullName},</p>
                                <p>Good day!</p>
                    
                                {messageContent}

                                <div class='system-links'>
                                    <p><strong>Please use the link above to access the system:</strong></p>                                
                                </div>

                                
                            </div>
                
                            <div class='footer'>
                                <p>This is an automated message. Please do not reply to this email.</p>
                                <p>&copy; {DateTime.Now.Year} Sanyo Denki Phil. All rights reserved.</p>
                            </div>
                        </div>
                    </body>
                    </html>";

            return emailBody;
        }





        public static string CreatePatrolProductionBody(
           PatrolRegistrationViewModel patrol,
           List<FindingModel> find, 
           string link)
        {
            string messageContent = "";
            string findings = "";
            string Countermeasure = "";

            // Build findings list
            foreach (var item in find)
            {
                findings += "<li>" + item.FindDescription + "</li>";
            }

            // Build countermeasures list - show "No Countermeasure yet" if empty
            foreach (var item in find)
            {
                if (string.IsNullOrEmpty(item.Countermeasure))
                {
                    Countermeasure += "<li><em>No Countermeasure yet</em></li>";
                }
                else
                {
                    Countermeasure += "<li>" + item.Countermeasure + "</li>";
                }
            }

            // If no findings at all, show a default message
            if (string.IsNullOrEmpty(findings))
            {
                findings = "<li><em>No findings reported</em></li>";
            }


            messageContent = $@"
                    <p>This is to inform you that you have a response and are <strong>required to submit a countermeasure</strong> for the Patrol Inspection report.</p>

                    <table class='details-table'>
                        <tr>
                            <td class='label'>Registration No:</td>
                            <td><strong>{patrol.RegNo}</strong></td>
                        </tr>
                        <tr>
                            <td class='label'>Inspection / Findings:</td>
                            <td>
                                <ol>
                                    {findings}
                                </ol>
                            </td>
                        </tr>
                        <tr>
                            <td class='label'>Counter Measures:</td>
                            <td>
                                <ol>
                                    {Countermeasure}
                                </ol>
                            </td>
                        </tr>
                        <tr>
                            <td class='label'>Person Incharge:</td>
                            <td>{patrol.PICName}</td>
                        </tr>
                        <tr>
                            <td class='label'>Approve By Inspector:</td>
                            <td>{patrol.InspectName}</td>
                        </tr>
                        <tr>
                            <td class='label'>Link of the Registration No.:</td>
                            <a href='{link}' class='button'>Submit Countermeasures</a>
                        </tr>
                    </table>";


            string emailBody = $@"
                <html>
                <head>
                    <style>
                        body {{ font-family: 'Segoe UI', Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0; background-color: #f5f5f5; }}
                        .container {{ max-width: 700px; margin: 0 auto; padding: 0; background: white; }}
                        .header {{ background: linear-gradient(135deg, #dc3545, #c82333); color: white; padding: 20px; }}
                        .header h2 {{ margin: 0; font-size: 18px; font-weight: bold; }}
                        .email-info {{ background: #e9ecef; padding: 8px 20px; font-size: 11px; color: #666; border-bottom: 1px solid #ddd; }}
                        .content {{ padding: 25px !important; }}
                        .button {{ 
                            display: inline-block; 
                            padding: 12px 24px; 
                            background-color: #dc3545; 
                            color: white; 
                            text-decoration: none; 
                            border-radius: 4px; 
                            font-weight: bold; 
                            margin: 5px 0;
                        }}
                        .footer {{ margin-top: 25px; padding-top: 20px; border-top: 1px solid #e9ecef; font-size: 11px; color: #6c757d; }}
                        .details-table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
                        .details-table td {{ padding: 12px; border: 1px solid #dee2e6; vertical-align: top; }}
                        .details-table tr:nth-child(even) {{ background-color: #f8f9fa; }}
                        .label {{ font-weight: bold; color: #495057; width: 25%; background-color: #f8f9fa; }}
                        .system-links {{ background: #e7f3ff; padding: 15px; border-radius: 4px; margin: 20px 0; }}
                        .note-box {{ background: #fff3cd; border: 1px solid #ffeaa7; border-radius: 4px; padding: 12px; margin: 15px 0; font-size: 12px; }}
                        .browser-notice {{ font-size: 11px; color: #666; text-align: center; margin-bottom: 20px; }}
                        ol {{ margin: 10px 0; padding-left: 20px; }}
                        li {{ margin-bottom: 8px; line-height: 1.5; }}
                    </style>
                </head>
                <body>
                    <div class='container'>
                        <div class='header'>
                            <h2>[PATROL INSPECTION] Countermeasure Required - {patrol.RegNo}</h2>
                        </div>

                        <div class='content'>
                            <p>Hi {patrol.PICName},</p>
                            <p>Good day!</p>
        
                            {messageContent}

                         

                            <div class='note-box'>
                                <p><strong>Important:</strong> Please review the findings and submit appropriate countermeasures within the required timeframe.</p>
                            </div>
                        </div>
    
                        <div class='footer'>
                            <p>This is an automated message. Please do not reply to this email.</p>
                            <p>&copy; {DateTime.Now.Year} Sanyo Denki Phil. All rights reserved.</p>
                        </div>
                    </div>
                </body>
                </html>";

            return emailBody;
        }
    }
}