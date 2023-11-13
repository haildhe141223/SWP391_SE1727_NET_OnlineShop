﻿namespace SWP391.OnlineShop.Common.Templates;

public class EmailTemplates
{
    #region Forgot Password Notification

    public static string ForgotPasswordSubject = @"Karma Shop - Reset Password Notification";
    public static string ForgotPasswordTemplate = @"
                            <table width=""600"" style=""width:100%;border-collapse:collapse;font-family: arial,helvetica,sans-serif;"">
                              <tbody>
                                <tr>
                                  <td style=""padding:30px 0 0 0;background:#ffffff;color:#1a2533;font-size:16px"">
                                    <table width=""100%"" style=""border-collapse:collapse;width:100%;margin:0px;padding:0px;color:inherit"" cellspacing=""0"" cellpadding=""0"">
                                      <tbody>
                                        <tr style=""color:#1a2533"">
                                          <td style=""padding:0 36px;font-size:16px"">
                                            <p> Dear, {{Username}} </p>
                                            <p style=""margin-bottom:30px;font-weight:600""> A request has been received to change the password for your account - <b>{{Username}}</b> (<a href=""mailto:{{UserEmail}}"" style=""color:#1389fe;text-decoration:none"" target=""_blank"">{{UserEmail}}</a>) </p>
                                            <p style=""text-align:center;margin-bottom:40px"">
                                              <a style=""background:#1389fe;display:inline-block;padding:14px 0;color:#fff;font-size:16px;text-decoration:none;line-height:18px;width:200px"" target=""_blank"" href=""{{ResetPassLink}}""> Reset Password </a>
                                            </p>
                                            <p> If you did not initiate this request. Please contact us by reply this email.</p>
                                            <p style=""margin-top:50px ""> Thank you and warmest regards, </p>
                                            <p> Karma Shop Teams </p>
                                          </td>
                                        </tr>
                                    </table>
                                <tr>
                                  <td style=""text-align:center;color:#5e5e5e;font-size:10px;padding:20px 36px 0 36px""> This message was sent to <a style=""color:#1a2533;text-decoration:none"" href=""mailto:{{UserEmail}}"" target=""_blank"">{{UserEmail}}</a>, and this email is auto-generated by the system, please do not reply. </td>
                                </tr>
                                <tr>
                                  <td style=""padding-top:5px;text-align:center;color:#1a2533;font-size:10px""></td>
                                </tr>
                                <tr>
                                  <td style=""border-bottom-left-radius:36px;border-bottom-right-radius:36px;padding:5px 0;font-size:10px;text-align:center;color:#5e5e5e;border-bottom-left-radius:30px;border-bottom-right-radius:30px"">2023 © Karma Shop</td>
                                </tr>
                              </tbody>
                            </table>";

    #endregion

    #region Email Confirm Notification

    public static string RegisterEmailConfirmSubject = @"Karma Shop - Confirm Register Email";
    public static string RegisterEmailConfirmTemplate = @"
                            <table width=""600"" style=""width:100%;border-collapse:collapse;font-family: arial,helvetica,sans-serif;"">
                              <tbody>
                                <tr>
                                  <td style=""padding:30px 0 0 0;background:#ffffff;color:#1a2533;font-size:16px"">
                                    <table width=""100%"" style=""border-collapse:collapse;width:100%;margin:0px;padding:0px;color:inherit"" cellspacing=""0"" cellpadding=""0"">
                                      <tbody>
                                        <tr style=""color:#1a2533"">
                                          <td style=""padding:0 36px;font-size:16px"">
                                            <p> Dear, {{Username}} </p>
                                            <p style=""margin-bottom:30px;font-weight:600""> This email already registed in our web site - Please confirm your email address - <b>{{Username}}</b> (<a href=""mailto:{{UserEmail}}"" style=""color:#1389fe;text-decoration:none"" target=""_blank"">{{UserEmail}}</a>) </p>
                                            <p style=""text-align:center;margin-bottom:40px"">
                                              <a style=""background:#1389fe;display:inline-block;padding:14px 0;color:#fff;font-size:16px;text-decoration:none;line-height:18px;width:200px"" target=""_blank"" href=""{{ConfirmRegisterLink}}""> Confirm Register Email </a>
                                            </p>
                                            <p> If you did not initiate this request. Please contact us by reply this email.</p>
                                            <p style=""margin-top:50px ""> Thank you and warmest regards, </p>
                                            <p> Karma Shop Teams </p>
                                          </td>
                                        </tr>
                                    </table>
                                <tr>
                                  <td style=""text-align:center;color:#5e5e5e;font-size:10px;padding:20px 36px 0 36px""> This message was sent to <a style=""color:#1a2533;text-decoration:none"" href=""mailto:{{UserEmail}}"" target=""_blank"">{{UserEmail}}</a>, and this email is auto-generated by the system, please do not reply. </td>
                                </tr>
                                <tr>
                                  <td style=""padding-top:5px;text-align:center;color:#1a2533;font-size:10px""></td>
                                </tr>
                                <tr>
                                  <td style=""border-bottom-left-radius:36px;border-bottom-right-radius:36px;padding:5px 0;font-size:10px;text-align:center;color:#5e5e5e;border-bottom-left-radius:30px;border-bottom-right-radius:30px"">2023 © Karma Shop</td>
                                </tr>
                              </tbody>
                            </table>";
    public static string RegisterEmailConfirmWithPasswordTemplate = @"
                            <table width=""600"" style=""width:100%;border-collapse:collapse;font-family: arial,helvetica,sans-serif;"">
                              <tbody>
                                <tr>
                                  <td style=""padding:30px 0 0 0;background:#ffffff;color:#1a2533;font-size:16px"">
                                    <table width=""100%"" style=""border-collapse:collapse;width:100%;margin:0px;padding:0px;color:inherit"" cellspacing=""0"" cellpadding=""0"">
                                      <tbody>
                                        <tr style=""color:#1a2533"">
                                          <td style=""padding:0 36px;font-size:16px"">
                                            <p> Dear, {{Username}} </p>
                                            <p style=""margin-bottom:30px;font-weight:600""> This email already registed in our web site - Please confirm your email address - <b>{{Username}}</b> ( <a href=""mailto:{{UserEmail}}"" style=""color:#1389fe;text-decoration:none"" target=""_blank"">{{UserEmail}}</a>) </p>
                                            <p style=""text-align:center;margin-bottom:40px"">
                                              <a style=""background:#1389fe;display:inline-block;padding:14px 0;color:#fff;font-size:16px;text-decoration:none;line-height:18px;width:200px"" target=""_blank"" href=""{{ConfirmRegisterLink}}""> Confirm Register Email </a>
                                            </p>
                                            <p>
                                              <strong> Your default password is: </strong> {{DefaultPassword}}.
                                            </p>
                                            <p> If you did not initiate this request. Please contact us by reply this email.</p>
                                            <p style=""margin-top:50px ""> Thank you and warmest regards, </p>
                                            <p> Karma Shop Teams </p>
                                          </td>
                                        </tr>
                                    </table>
                                <tr>
                                  <td style=""text-align:center;color:#5e5e5e;font-size:10px;padding:20px 36px 0 36px""> This message was sent to <a style=""color:#1a2533;text-decoration:none"" href=""mailto:{{UserEmail}}"" target=""_blank"">{{UserEmail}}</a>, and this email is auto-generated by the system, please do not reply. </td>
                                </tr>
                                <tr>
                                  <td style=""padding-top:5px;text-align:center;color:#1a2533;font-size:10px""></td>
                                </tr>
                                <tr>
                                  <td style=""border-bottom-left-radius:36px;border-bottom-right-radius:36px;padding:5px 0;font-size:10px;text-align:center;color:#5e5e5e;border-bottom-left-radius:30px;border-bottom-right-radius:30px"">2023 © Karma Shop</td>
                                </tr>
                              </tbody>
                            </table>";


    #endregion
}