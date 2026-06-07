using FinTrack.Application.Requests.Consolidate.Portfolio.Interfaces;
using FinTrack.Application.Requests.Consolidate.Portfolio.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure.Services.ConsolidatedService
{
    public class MarginHelperService : IMarginHelperService
    {
        public CustomMessageVM GetMarginActionMessage(string Risk_Status, decimal Depo_Buy_Req, decimal Adjust_Req, decimal Sell_Req, decimal Penal_Fee_Start_LTV, decimal Authorized_LTV, decimal marCalLTV, decimal marCalTargetLTV, decimal liqLTV, decimal liqTargetLTV, decimal ShareMarketValue, decimal Loan)
        {
            CustomMessageVM objCustomMessage = new CustomMessageVM();

            if (Risk_Status == "normal")
            {
                objCustomMessage.CustomMessageHeader = "Normal";
                objCustomMessage.CustomMessageBody = "আপনার অ্যাকাউন্টের অবস্থা স্বাভাবিক।";
                objCustomMessage.ColorCode = "#e1eddd";
                objCustomMessage.FontColorCode = "#242a30";
            }
            else if (Risk_Status == "limit_crossed")
            {
                objCustomMessage.CustomMessageHeader = "Limit Exceeded – Action Required";
                objCustomMessage.CustomMessageBody = $"আপনার ঋণ অনুমোদিত সীমা অতিক্রম করেছে। অনুগ্রহপূর্বক আপনার ঋণ কমিয়ে অনুমোদিত LTV <b>{string.Format("{0:0}%", Authorized_LTV * 100)}</b> এর মধ্যে নিয়ে আসুন।";
                objCustomMessage.ColorCode = "#3BFCFF";
                objCustomMessage.FontColorCode = "#242a30";
            }
            else if (Risk_Status == "penal_warning")
            {
                objCustomMessage.CustomMessageHeader = "Over-Usage Fee Warning – Action Required";
                objCustomMessage.CustomMessageBody = $"আপনার ঋণ গুরুতর ভাবে অনুমদিত সীমা লঙ্ঘন করেছে। এমতাবস্থায় চুক্তি অনুযায়ী আপনার অ্যাকাউন্টে Loan Over-Usage Fee আরোপিত হতে পারে। আপনি সন্মানিত বিনিয়োগকারী বিধায় এই মুহূর্তে Loan Over-Usage Fee আরোপিত করা হচ্ছে না। তবে, আপনার LTV <b>{string.Format("{0:0}%", Penal_Fee_Start_LTV * 100)}</b> অতিক্রম করলে প্রতি মাসে Loan Over-Usage Fee আরোপিত হবে। অনুগ্রহপূর্বক আপনার ঋণ কমিয়ে অনুমোদিত LTV <b>{string.Format("{0:0}%", Authorized_LTV * 100)}</b> এর মধ্যে নিয়ে আসুন এবং Loan Over-Usage Fee থেকে মুক্ত থাকুন।";
                objCustomMessage.ColorCode = "#FFC300";
                objCustomMessage.FontColorCode = "#242a30";
            }
            else if (Risk_Status == "penal_fee_impose")
            {
                objCustomMessage.CustomMessageHeader = "Risky – Action Required";
                objCustomMessage.ColorCode = "#FF5733";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে। এমতাবস্থায় ঋণের চুক্তি অনুযায়ী আপনার অ্যাকাউন্টে প্রতি মাসে Loan Over-Usage Fee আরোপিত হচ্ছে। অনুগ্রহপূর্বক নিম্নের একটি পন্থা অবলম্বন করে আপনার ঋণ কমিয়ে অনুমোদিত LTV <b>" + string.Format("{0:0}%", Authorized_LTV * 100) + @"</b> এর মধ্যে নিয়ে আসুন এবং Loan Over-Usage Fee থেকে মুক্ত থাকুন। </p>
                                   <ol>
                                       <li>নতুন ভাবে টাকা জমা করে <b>" + string.Format("{0:n} Tk", Depo_Buy_Req) + @"</b> মূল্যমানের শেয়ার ক্রয় করুন নতুবা সমমূল্যের শেয়ার জমা করুন, অথবা</li>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Sell_Req) + @"</b> মূল্যমানের শেয়ার বিক্রয় করুন।</li>
                                    </ol>
                                   <p> উল্লেখ্যঃ আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", marCalLTV * 100) + @"</b> এর উপরে গেলে আপনার শেয়ার ক্রয় সুবিধা স্থগিত করা হবে।</p>";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে। এমতাবস্থায় ঋণের চুক্তি অনুযায়ী আপনার অ্যাকাউন্টে প্রতি মাসে Loan Over-Usage Fee আরোপিত হচ্ছে। অনুগ্রহপূর্বক নিম্নের একটি পন্থা অবলম্বন করে আপনার ঋণ কমিয়ে অনুমোদিত LTV <b>" + string.Format("{0:0}%", Authorized_LTV * 100) + @"</b> এর মধ্যে নিয়ে আসুন এবং Loan Over-Usage Fee থেকে মুক্ত থাকুন। </p>
                                   <ol>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Sell_Req) + @"</b> মূল্যমানের শেয়ার বিক্রয় করুন।</li>
                                    </ol>
                                   <p> উল্লেখ্যঃ আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", marCalLTV * 100) + @"</b> এর উপরে গেলে আপনার শেয়ার ক্রয় সুবিধা স্থগিত করা হবে।</p>";

                }
            }
            else if (Risk_Status == "margin_call")
            {
                objCustomMessage.CustomMessageHeader = "Margin Call – Urgent Action Required";
                objCustomMessage.ColorCode = "#C1000C";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে এবং চুক্তি অনুযায়ী আপনার ঋণ সমন্বয়ের আহব্বান (Margin Call) করা হচ্ছে। এমতাবস্থায় আপনার স্বাভাবিক শেয়ার ক্রয় সুবিধা স্থগিত করা হয়েছে। শুধুমাত্র শেয়ার বিক্রয় করলে বিক্রিত পরিমানের <b>৯০%</b> পর্যন্ত ক্রয় সুবিধা পেতে পারেন।  অনুগ্রহপূর্বক স্বাভাবিক শেয়ার ক্রয় সুবিধা ফিরে পেতে নিম্নের একটি পন্থা অবলম্বন করে আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", marCalTargetLTV * 100) + @"</b> এর নিচে নিয়ে আসুন এবং Liquidation (Force Sell) এর ঝুকিমুক্ত থাকুন। </p>
                                   <ol>
                                       <li>নতুন ভাবে টাকা জমা করে <b>" + string.Format("{0:n} Tk", Depo_Buy_Req) + @"</b> মূল্যমানের শেয়ার ক্রয় করুন নতুবা সমমূল্যের শেয়ার জমা করুন, অথবা</li>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Sell_Req) + @"</b> মূল্যমানের শেয়ার বিক্রয় করুন।</li>
                                    </ol>
                                    <p>উল্লেখ্য, আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", liqLTV * 100) + @"</b> এর উপরে যাওয়ামাত্র বিনা নোটিশে শেয়ার বিক্রয় করে বাধ্যতামূলকভাবে ঋণ সমন্বয় করা হবে।</p>";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে এবং চুক্তি অনুযায়ী আপনার ঋণ সমন্বয়ের আহব্বান (Margin Call) করা হচ্ছে। এমতাবস্থায় আপনার স্বাভাবিক। শেয়ার ক্রয় সুবিধা স্থগিত করা হয়েছে। শুধুমাত্র শেয়ার বিক্রয় করলে বিক্রিত পরিমানের <b>৯০%</b> পর্যন্ত ক্রয় সুবিধা পেতে পারেন।  অনুগ্রহপূর্বক স্বাভাবিক শেয়ার ক্রয় সুবিধা ফিরে পেতে নিম্নের একটি পন্থা অবলম্বন করে আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", marCalTargetLTV * 100) + @"</b> এর নিচে নিয়ে আসুন এবং Liquidation (Force Sell) এর ঝুকিমুক্ত থাকুন। </p>
                                   <ol>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Sell_Req) + @"</b> মূল্যমানের শেয়ার বিক্রয় করুন।</li>
                                    </ol>
                                    <p>উল্লেখ্য, আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", liqLTV * 100) + @"</b> এর উপরে যাওয়ামাত্র বিনা নোটিশে শেয়ার বিক্রয় করে বাধ্যতামূলকভাবে ঋণ সমন্বয় করা হবে।</p>";

                }
            }
            else if (Risk_Status == "liquidation")
            {
                objCustomMessage.CustomMessageHeader = "Liquidation – Urgent Action Required";
                objCustomMessage.ColorCode = "#C1000C";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ খুবই বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে। আপনার অ্যাকাউন্টের শেয়ার যেকোনো মুহূর্তে বিনা নোটিশে বিক্রয় করে ঋণ সমন্বয় করা হবে। জরুরীভিত্তিতে নিম্নের একটি পন্থা অবলম্বন করে আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", liqTargetLTV * 100) + @"</b> এর নিচে নিয়ে আসুন এবং Liquidation (Force Sell) এর ঝুকিমুক্ত থাকুন। </p>
                                   <ol>
                                       <li>নতুন ভাবে টাকা জমা করে <b>" + string.Format("{0:n} Tk", Depo_Buy_Req) + @"</b> মূল্যমানের শেয়ার ক্রয় করুন নতুবা সমমূল্যের শেয়ার জমা করুন, অথবা</li>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Sell_Req) + @"</b> মূল্যমানের শেয়ার বিক্রয় করুন।</li>
                                    </ol>";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ খুবই বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে। আপনার অ্যাকাউন্টের শেয়ার যেকোনো মুহূর্তে বিনা নোটিশে বিক্রয় করে ঋণ সমন্বয় করা হবে। জরুরীভিত্তিতে নিম্নের একটি পন্থা অবলম্বন করে আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", liqTargetLTV * 100) + @"</b> এর নিচে নিয়ে আসুন এবং Liquidation (Force Sell) এর ঝুকিমুক্ত থাকুন। </p>
                                   <ol>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Sell_Req) + @"</b> মূল্যমানের শেয়ার বিক্রয় করুন।</li>
                                    </ol>";
                }
            }
            else if (Risk_Status == "equity_minus")
            {
                objCustomMessage.CustomMessageHeader = "Equity Minus – Urgent Action Required";
                objCustomMessage.ColorCode = "#C1000C";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ খুবই বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে। আপনার অ্যাকাউন্টের শেয়ার যেকোনো মুহূর্তে বিনা নোটিশে বিক্রয় করে ঋণ সমন্বয় করা হবে। জরুরীভিত্তিতে নিম্নের একটি পন্থা অবলম্বন করে আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", liqTargetLTV * 100) + @"</b> এর নিচে নিয়ে আসুন এবং Liquidation (Force Sell) এর ঝুকিমুক্ত থাকুন। </p>
                                   <ol>
                                       <li>নতুন ভাবে টাকা জমা করে <b>" + string.Format("{0:n} Tk", Depo_Buy_Req) + @"</b> মূল্যমানের শেয়ার ক্রয় করুন নতুবা সমমূল্যের শেয়ার জমা করুন, অথবা</li>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Loan - ShareMarketValue) + @"</b> জমা করুন এবং সকল শেয়ার বিক্রয় করুন।</li>
                                    </ol>";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"<p>আপনার ঋণ খুবই বিপদজনক ভাবে অনুমোদিত সীমা লঙ্ঘন করেছে। আপনার অ্যাকাউন্টের শেয়ার যেকোনো মুহূর্তে বিনা নোটিশে বিক্রয় করে ঋণ সমন্বয় করা হবে। জরুরীভিত্তিতে নিম্নের একটি পন্থা অবলম্বন করে আপনার বর্তমান LTV <b>" + string.Format("{0:0}%", liqTargetLTV * 100) + @"</b> এর নিচে নিয়ে আসুন এবং Liquidation (Force Sell) এর ঝুকিমুক্ত থাকুন। </p>
                                   <ol>
                                       <li>শুধুমাত্র টাকা জমা করে <b>" + string.Format("{0:n} Tk", Adjust_Req) + @"</b> ঋণ কমিয়ে নিয়ে আসুন, অথবা</li>
                                       <li><b>" + string.Format("{0:n} Tk", Loan - ShareMarketValue) + @"</b> জমা করুন এবং সকল শেয়ার বিক্রয় করুন।</li>
                                    </ol>";
                }
            }
            else
            {
                objCustomMessage.CustomMessageHeader = "Unknown";
                objCustomMessage.CustomMessageBody = "আপনার অ্যাকাউন্টের অবস্থা স্বাভাবিক।";
                objCustomMessage.ColorCode = "#e1eddd";
                objCustomMessage.FontColorCode = "#242a30";
            }

            return objCustomMessage;
        }
        public CustomMessageVM GetMarginActionMessageforPDF(string Risk_Status, decimal Depo_Buy_Req, decimal Adjust_Req, decimal Sell_Req, decimal Penal_Fee_Start_LTV, decimal Authorized_LTV, decimal marCalLTV, decimal marCalTargetLTV, decimal liqLTV, decimal liqTargetLTV, decimal ShareMarketValue, decimal Loan)
        {
            CustomMessageVM objCustomMessage = new CustomMessageVM();

            if (Risk_Status == "normal")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Normal";
                objCustomMessage.CustomMessageBody = "Avcbvi FY Aby‡gvw`Z mxgvi g‡a¨ Av‡Q| GB gyn~‡Z© ‡Kvb c`‡¶‡ci cÖ‡qvRb ‡bB|";
                objCustomMessage.ColorCode = "#e1eddd";
                objCustomMessage.FontColorCode = "#242a30";
            }
            else if (Risk_Status == "limit_crossed")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Limit Exceeded – Action Required";
                objCustomMessage.CustomMessageBody = $"Avcbvi FY Aby‡gvw`Z mxgv AwZµg K‡i‡Q| AbyMÖnc~e©K Avcbvi FY Kwg‡q Aby‡gvw`Z Gj.wU.wf {string.Format("{0:0}%", Authorized_LTV * 100)} Gi g‡a¨ wb‡q Avmyb|";
                objCustomMessage.ColorCode = "#3BFCFF";
                objCustomMessage.FontColorCode = "#242a30";
            }
            else if (Risk_Status == "penal_warning")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Over-Usage Fee Warning – Action Required";
                objCustomMessage.CustomMessageBody = $"Avcbvi FY ¸iæZi fv‡e mxgv j•Nb K‡i‡Q| GgZve¯’vq Pzw³ Abyhvqx Avcbvi A¨vKvD‡›U ‡jvb Ifvi-BD‡Rm wd Av‡ivwcZ n‡Z cv‡i| Avcwb mb¥vwbZ wewb‡qvMKvix weavq GB gyn~‡Z© ‡jvb Ifvi-BD‡Rm wd Av‡ivwcZ Kiv n‡”Q bv| Z‡e Avcbvi Gj.wU.wf {string.Format("{0:0}%", Penal_Fee_Start_LTV * 100)} AwZµg Ki‡jcÖwZ gv‡m ‡jvb Ifvi-BD‡Rm wd Av‡ivwcZ n‡e| AbyMÖnc~e©K Avcbvi FY Kwg‡q Aby‡gvw`Z Gj.wU.wf {string.Format("{0:0}%", Authorized_LTV * 100)} Gi g‡a¨ wb‡q Avmyb Ges ‡jvb Ifvi-BD‡Rm wd ‡_‡K gy³ _vKzb|";
                objCustomMessage.ColorCode = "#FFC300";
                objCustomMessage.FontColorCode = "#242a30";
            }
            else if (Risk_Status == "penal_fee_impose")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Risky – Action Required";
                objCustomMessage.ColorCode = "#FF5733";
                objCustomMessage.FontColorCode = "#FFFFFF";
                objCustomMessage.CustomMessageBody = "";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY wec`RbK  fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q| GgZve¯’vq F‡bi Pzw³ Abyhvqx Avcbvi A¨vKvD‡›UcÖwZ gv‡m ‡jvb Ifvi-BD‡Rm wd Av‡ivwcZ n‡”Q| AbyMÖnc~e©K wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi FY Kwg‡q Aby‡gvw`Z Gj.wU.wf " + string.Format("{0:0}%", Authorized_LTV * 100) + @"Gi g‡a¨ wb‡q Avmyb Ges ‡jvb Ifvi-BD‡Rm wd ‡_‡K gy³ _vKzb|
    1.	bZzb fv‡e UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Depo_Buy_Req) + @" gyj¨gv‡bi †kqvi µq Kiæb bZyev mggy‡j¨i ‡kqvi Rgv Kiæb, A_ev 
    2.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    3.	" + string.Format("{0:n} UvKv", Sell_Req) + @" gyj¨gv‡bi †kqvi weµq Kiæb|
D‡jøL¨, Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", marCalLTV * 100) + @" Gi Dc‡i ‡M‡j Avcbvi †kqvi µq myweav ¯’wMZ Kiv n‡e|";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY wec`RbK  fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q| GgZve¯’vq F‡bi Pzw³ Abyhvqx Avcbvi A¨vKvD‡›UcÖwZ gv‡m ‡jvb Ifvi-BD‡Rm wd Av‡ivwcZ n‡”Q| AbyMÖnc~e©K wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi FY Kwg‡q Aby‡gvw`Z Gj.wU.wf " + string.Format("{0:0}%", Authorized_LTV * 100) + @"Gi g‡a¨ wb‡q Avmyb Ges ‡jvb Ifvi-BD‡Rm wd ‡_‡K gy³ _vKzb|
    1.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    2.	" + string.Format("{0:n} UvKv", Sell_Req) + @" gyj¨gv‡bi †kqvi weµq Kiæb|
D‡jøL¨, Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", marCalLTV * 100) + @" Gi Dc‡i ‡M‡j Avcbvi †kqvi µq myweav ¯’wMZ Kiv n‡e|";
                }

            }
            else if (Risk_Status == "margin_call")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Margin Call – Urgent Action Required";
                objCustomMessage.ColorCode = "#C1000C";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY wec`RbK fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q Ges Pzw³ Abyhvqx Avcbvi FY mgš^‡qi AvneŸvb (gvwR©b Kj) Kiv n‡”Q| GgZve¯’vq Avcbvi ¯^vfvweK †kqvi µq myweav ¯’wMZ Kiv n‡q‡Q| ïaygvÎ †kqvi weµq Ki‡j wewµZ cwigv‡bi 90% µq myweav †c‡Z cv‡ib| AbyMÖnc~e©K ¯^vfvweK †kqvi µq myweav wd‡i †c‡Z wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", marCalTargetLTV * 100) + @"Gi wb‡P wb‡q Avmyb Ges wjKzB‡Wkb  (†dvm© †mj) Gi SzwKgy³ _vKzb|
    1.	bZzb fv‡e UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Depo_Buy_Req) + @" gyj¨gv‡bi †kqvi µq Kiæb bZyev mggy‡j¨i ‡kqvi Rgv Kiæb, A_ev 
    2.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    3.	" + string.Format("{0:n} UvKv", Sell_Req) + @" gyj¨gv‡bi †kqvi weµq Kiæb|
D‡jøL¨, Avcbvi eZ©gvb Gj.wU.wf  " + string.Format("{0:0}%", liqLTV * 100) + @" Gi Dc‡i hvIqvgvG webv †bvwU‡k †kqvi weµq K‡I eva¨Zvg~jKfv‡e mgš^q Kiv n‡e|";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY wec`RbK fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q Ges Pzw³ Abyhvqx Avcbvi FY mgš^‡qi AvneŸvb (gvwR©b Kj) Kiv n‡”Q| GgZve¯’vq Avcbvi ¯^vfvweK †kqvi µq myweav ¯’wMZ Kiv n‡q‡Q| ïaygvÎ †kqvi weµq Ki‡j wewµZ cwigv‡bi 90% µq myweav †c‡Z cv‡ib| AbyMÖnc~e©K ¯^vfvweK †kqvi µq myweav wd‡i †c‡Z wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", marCalTargetLTV * 100) + @"Gi wb‡P wb‡q Avmyb Ges wjKzB‡Wkb  (†dvm© †mj) Gi SzwKgy³ _vKzb|
    1.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    2.	" + string.Format("{0:n} UvKv", Sell_Req) + @" gyj¨gv‡bi †kqvi weµq Kiæb|
D‡jøL¨, Avcbvi eZ©gvb Gj.wU.wf  " + string.Format("{0:0}%", liqLTV * 100) + @" Gi Dc‡i hvIqvgvG webv †bvwU‡k †kqvi weµq K‡I eva¨Zvg~jKfv‡e mgš^q Kiv n‡e|";
                }
            }
            else if (Risk_Status == "liquidation")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Liquidation – Urgent Action Required";
                objCustomMessage.ColorCode = "#C1000C";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY wec`RbK fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q| Avcbvi A¨vKvD‡›Ui †kqvi †h‡Kv‡bv gyn~‡Z© webv †bvwU‡k weµq K‡i FY mgš^q Kiv n‡e| RiæixwfwI‡Z wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", liqTargetLTV * 100) + @" Gi wb‡P wb‡q Avmyb Ges wjKzB‡Wkb  (†dvm© †mj) Gi SzwKgy³ _vKzb|
    1.	bZzb fv‡e UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Depo_Buy_Req) + @" gyj¨gv‡bi †kqvi µq Kiæb bZyev mggy‡j¨i ‡kqvi Rgv Kiæb, A_ev 
    2.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    3.	" + string.Format("{0:n} UvKv", Sell_Req) + @" gyj¨gv‡bi †kqvi weµq Kiæb|";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY wec`RbK fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q| Avcbvi A¨vKvD‡›Ui †kqvi †h‡Kv‡bv gyn~‡Z© webv †bvwU‡k weµq K‡i FY mgš^q Kiv n‡e| RiæixwfwI‡Z wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", liqTargetLTV * 100) + @" Gi wb‡P wb‡q Avmyb Ges wjKzB‡Wkb  (†dvm© †mj) Gi SzwKgy³ _vKzb| 
    1.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    2.	" + string.Format("{0:n} UvKv", Sell_Req) + @" gyj¨gv‡bi †kqvi weµq Kiæb|";
                }
            }
            else if (Risk_Status == "equity_minus")
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Equity Minus – Urgent Action Required";
                objCustomMessage.ColorCode = "#C1000C";
                objCustomMessage.FontColorCode = "#FFFFFF";
                if (Authorized_LTV > 0)
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY LyeB wec`RbK fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q| Avcbvi A¨vKvD‡›Ui †kqvi †h‡Kv‡bv gyn~‡Z© webv †bvwU‡k weµq K‡i FY mgš^q Kiv n‡e| RiæixwfwI‡Z wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", liqTargetLTV * 100) + @" Gi wb‡P wb‡q Avmyb Ges wjKzB‡Wkb  (†dvm© †mj) Gi SzwKgy³ _vKzb|
    1.	bZzb fv‡e UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Depo_Buy_Req) + @" gyj¨gv‡bi †kqvi µq Kiæb bZyev mggy‡j¨i ‡kqvi Rgv Kiæb, A_ev 
    2.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    3.	" + string.Format("{0:n} UvKv", Loan - ShareMarketValue) + @" Rgv Kiæb Ges mKj †kqvi weµq Kiæb|";
                }
                else
                {
                    objCustomMessage.CustomMessageBody = @"Avcbvi FY LyeB wec`RbK fv‡e Aby‡gvw`Z mxgv j•Nb K‡i‡Q| Avcbvi A¨vKvD‡›Ui †kqvi †h‡Kv‡bv gyn~‡Z© webv †bvwU‡k weµq K‡i FY mgš^q Kiv n‡e| RiæixwfwI‡Z wb‡¤œi GKwU cš’v Aej¤^b K‡i Avcbvi eZ©gvb Gj.wU.wf " + string.Format("{0:0}%", liqTargetLTV * 100) + @" Gi wb‡P wb‡q Avmyb Ges wjKzB‡Wkb  (†dvm© †mj) Gi SzwKgy³ _vKzb|
    1.	ïaygvÎ UvKv Rgv K‡i " + string.Format("{0:n} UvKv ", Adjust_Req) + @" FY Kwg‡q wb‡q Avmyb, A_ev
    2.	" + string.Format("{0:n} UvKv", Loan - ShareMarketValue) + @" Rgv Kiæb Ges mKj †kqvi weµq Kiæb|";
                }
            }
            else
            {
                objCustomMessage.CustomMessageHeader = "Account Health: Unknown";
                objCustomMessage.CustomMessageBody = "Avcbvi F‡Yi SywK ARvbv| AbyMªnc~e©K kxNªB Kv÷gvi †Kqv‡i †hvMv‡hvM Kiæb|";
                objCustomMessage.ColorCode = "#e1eddd";
                objCustomMessage.FontColorCode = "#242a30";
            }
            return objCustomMessage;
        }
        public MarginRiskStatusVM GetMarginRiskData(bool is_long_term, decimal AMR, decimal Loan, decimal EML, decimal ShareMarketValue, decimal penWarLTV, decimal marCalLTV, decimal marCalTargetLTV, decimal liqLTV, decimal Penal_Fee_Start_LTV, decimal Authorized_LTV, decimal LiqTargetLTV_Jamuna)
        {
            MarginRiskStatusVM objMarginRiskStatus = new MarginRiskStatusVM();
            var target_ltv = 0m;
            var Current_LTV = 0m;

            objMarginRiskStatus.Authorized_LTV = Authorized_LTV;

            if (ShareMarketValue > 0 && Loan > 0)
            {
                Current_LTV = Loan / ShareMarketValue;
                objMarginRiskStatus.Current_LTV = Current_LTV;
            }
            else if (ShareMarketValue <= 0 && Loan > 0)
            {
                Current_LTV = 9999;
                objMarginRiskStatus.Current_LTV = Current_LTV;
            }
            else
            {
                Current_LTV = 0;
                objMarginRiskStatus.Current_LTV = 0;
            }

            if (Current_LTV <= Authorized_LTV && Loan <= EML)
            {
                objMarginRiskStatus.Risk_Status = "normal";
            }
            else if ((Current_LTV > Authorized_LTV || Loan > EML) && Current_LTV < penWarLTV)
            {
                objMarginRiskStatus.Risk_Status = "limit_crossed";
            }
            else if (Current_LTV >= penWarLTV && Current_LTV < Penal_Fee_Start_LTV)
            {
                objMarginRiskStatus.Risk_Status = "penal_warning";
            }
            else if (Current_LTV >= Penal_Fee_Start_LTV && Current_LTV < marCalLTV)
            {
                objMarginRiskStatus.Risk_Status = "penal_fee_impose";
            }
            else if (Current_LTV >= marCalLTV && Current_LTV < liqLTV)
            {
                objMarginRiskStatus.Risk_Status = "margin_call";
            }
            else if (Current_LTV >= liqLTV && Current_LTV <= 1)
            {
                objMarginRiskStatus.Risk_Status = "liquidation";
            }
            else if (Current_LTV > 1)
            {
                objMarginRiskStatus.Risk_Status = "equity_minus";
            }
            else
            {
                objMarginRiskStatus.Risk_Status = "unknown";
            }


            if (objMarginRiskStatus.Risk_Status == "liquidation")
            {
                target_ltv = LiqTargetLTV_Jamuna;
            }
            else if (objMarginRiskStatus.Risk_Status == "margin_call")
            {
                target_ltv = marCalTargetLTV;
            }
            else
            {
                target_ltv = objMarginRiskStatus.Authorized_LTV;
            }

            objMarginRiskStatus.Depo_Buy_Req = target_ltv == 0 ? 0 : Math.Max(Loan / target_ltv - ShareMarketValue, 0);
            objMarginRiskStatus.Adjust_Req = Math.Max(Loan - ShareMarketValue * target_ltv, 0);
            objMarginRiskStatus.Sell_Req = 1 - target_ltv == 0 ? ShareMarketValue : Math.Max(((Current_LTV - target_ltv) * ShareMarketValue) / (1 - target_ltv), 0);

            return objMarginRiskStatus;
        }
    }
}
