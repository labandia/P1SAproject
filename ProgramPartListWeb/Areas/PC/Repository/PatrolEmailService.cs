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
                case "processowner":
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
    }
}