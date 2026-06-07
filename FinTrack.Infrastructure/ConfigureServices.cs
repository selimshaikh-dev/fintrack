using Ardalis.GuardClauses;
using FinTrack.Application.AuthRole.Interfaces;
using FinTrack.Application.AuthUser.Interfaces;
using FinTrack.Application.AuthView.Interfaces;
using FinTrack.Application.Commands.Base.Interfaces;
using FinTrack.Application.Common.Interfaces;
using FinTrack.Application.Requests.Base.BonusReceivable.Interfaces;
using FinTrack.Application.Requests.Base.Cash_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Client_Base.Interfaces;
using FinTrack.Application.Requests.Base.Client_Dividend.Interfaces;
using FinTrack.Application.Requests.Base.Ipo.Interfaces;
using FinTrack.Application.Requests.Base.MatureBalance.Interfaces;
using FinTrack.Application.Requests.Base.PortfolioAccountBalance.Interfaces;
using FinTrack.Application.Requests.Base.Share.Interfaces;
using FinTrack.Application.Requests.Consolidate.InterestCalculation.Interfaces;
using FinTrack.Application.Requests.Consolidate.LedgerDetails.Interfaces;
using FinTrack.Application.Requests.Consolidate.LedgerSummary.Interfaces;
using FinTrack.Application.Requests.Consolidate.Portfolio.Interfaces;
using FinTrack.Application.Requests.Consolidate.Purchase_Power.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Jamuna.Interfaces;
using FinTrack.Application.Requests.Jamuna.Client_Ledger_Details.Interfaces;
using FinTrack.Application.Requests.Jamuna.Employee.Interfaces;
using FinTrack.Application.Requests.Jamuna.Financial_Year.Interfaces;
using FinTrack.Application.Requests.Jamuna.MatureBalance.Interfaces;
using FinTrack.Application.Requests.Jamuna.Menus_Url.Interfaces;
using FinTrack.Application.Requests.Jamuna.TaxReport.Interfaces;
using FinTrack.Domain.Constants;
using FinTrack.Domain.Entities.Auth.AuthRole;
using FinTrack.Domain.Entities.Auth.AuthUser;
using FinTrack.Infrastructure.Data;
using FinTrack.Infrastructure.Data.Interceptors;
using FinTrack.Infrastructure.Identity;
using FinTrack.Infrastructure.Services;
using FinTrack.Infrastructure.Services.Base;
using FinTrack.Infrastructure.Services.ConsolidatedService;
using FinTrack.Infrastructure.Services.Jamuna;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinTrack.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());              
                options.UseSqlServer(connectionString, builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

            });
            services.AddTransient<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<ApplicationDbContextInitialiser>();

            IdentityBuilder builder = services.AddIdentityCore<ApplicationUser>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Password.RequireLowercase = true;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(ApplicationRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
            builder.AddRoleValidator<RoleValidator<ApplicationRole>>();
            builder.AddRoleManager<RoleManager<ApplicationRole>>();
            builder.AddSignInManager<SignInManager<ApplicationUser>>();

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddRoleManager<RoleManager<ApplicationRole>>()
                    .AddSignInManager<SignInManager<ApplicationUser>>()
                    .AddUserManager<UserManager<ApplicationUser>>()
                    .AddDefaultTokenProviders();


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {   // for development only

                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost:5000;https://localhost:5001;",
                    ValidAudience = "http://localhost:5000;https://localhost:5001;",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ecawiasqrpqrgyhwnolrudpbsrwaynbqdayndnmcehjnwqyouikpodzaqxivwkconwqbhrmxfgccbxbyljguwlxhdlcvxlutbnwjlgpfhjgqbegtbxbvwnacyqnltrby")),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
                options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));
            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddScoped<IServerDateTimeService, ServerDateTimeService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IViewRenderService, ViewRenderService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IClientServiceJamuna, ClientServiceJamuna>();
            services.AddScoped<IClientServiceJamuna, ClientServiceJamuna>();
            services.AddScoped<IClientBaseService, ClientBaseService>();
            services.AddScoped<IClientLedgerDetailsService, ClientLedgerDetailsService>();
            services.AddScoped<ITaxCertificateReportService, TaxCertificateReportService>();
            services.AddScoped<IFinancialYearService, FinancialYearService>();
            services.AddScoped<IShareBalanceService, ShareBalanceService>();
            services.AddScoped<IBonusReceivableService, BonusReceivableService>();
            services.AddScoped<IIpoApplicationService, IpoApplicationService>();
            services.AddScoped<IPortfolioAccountBalanceService, PortfolioAccountBalanceService>();
            services.AddScoped<IMarginHelperService, MarginHelperService>();
            services.AddScoped<ICashDividendService, CashDividendService>();
            services.AddScoped<IConsolidatedPurchasePowerService, ConsolidatedPurchasePowerService>();
            services.AddScoped<ICosolidatedPortfolioReportService, CosolidatedPortfolioReportService>();
            services.AddScoped<IInterestCalculationService, InterestCalculationService>();
            services.AddScoped<INextWorkingDateService, NextWorkingDateService>();
            services.AddScoped<IAccruedInterestDateService, AccruedInterestDateService>();
            services.AddScoped<IInterestCalculationDAL, InterestCalculationDAL>();
            services.AddScoped<IDailyActivityBalanceServiceJSCCL, DailyActivityBalanceServiceJSCCL>();
            services.AddScoped<IDailyActivityBalanceServiceGSL, DailyActivityBalanceServiceGSL>();
            services.AddScoped<IMatureBalanceServiceJSCCL, MatureBalanceServiceJSCCL>();
            services.AddScoped<IMarginInterestRateServiceJSCCL, MarginInterestRateServiceJSCCL>();
            services.AddScoped<IMatureBalanceServiceGSL, MatureBalanceServiceGSL>();
            services.AddScoped<IMarginInterestRateServiceGSL, MarginInterestRateServiceGSL>();
            services.AddScoped<IConsolidatedLedgerDetailsService, ConsolidatedLedgerDetailsService>();
            services.AddScoped<IConsolidatedLedgerSummaryService, ConsolidatedLedgerSummaryService>();
            services.AddScoped<IMenusUrlService, MenusUrlService>();
            services.AddScoped<IClientBalanceInfoService, ClientBalanceInfoService>();
            services.AddScoped<IInstrumentService, InstrumentService>();
            services.AddScoped<IClientCashDividendService, ClientCashDividendService>();
            services.AddScoped<IAddCashDividendService, AddCashDividendService>();
            return services;
        }
    }
}
