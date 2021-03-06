
/****** Object:  Table [dbo].[EarlyTermination]    Script Date: 11/3/2014 8:27:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EarlyTermination](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OfficeCode] [nvarchar](3) NOT NULL,
	[AppCode] [nvarchar](20) NOT NULL,
	[TransaksiNo] [nvarchar](20) NOT NULL,
	[CurrencyRate] [numeric](17, 2) NOT NULL,
	[EfectiveDate] [smalldatetime] NOT NULL,
	[RequestDate] [smalldatetime] NOT NULL,
	[PrepaymentAmount] [numeric](17, 2) NOT NULL,
	[TotalAmountToBePaid] [numeric](17, 2) NOT NULL,
	[OSPrincipal] [numeric](17, 2) NOT NULL,
	[OSInterest] [numeric](17, 2) NOT NULL,
	[OSPrincipalBank] [numeric](17, 2) NOT NULL,
	[OSInterestBank] [numeric](17, 2) NOT NULL,
	[OSInstallmentDue] [numeric](17, 2) NOT NULL,
	[OSInstallmentDueBank] [numeric](17, 2) NOT NULL,
	[OSInsuranceDue] [numeric](17, 2) NOT NULL,
	[OSLCInstallment] [numeric](17, 2) NOT NULL,
	[OSLCInsurance] [numeric](17, 2) NOT NULL,
	[LCInstallmentCurrent] [numeric](17, 2) NOT NULL,
	[LCInsuranceCurrent] [numeric](17, 2) NOT NULL,
	[PrepaymentFee] [numeric](17, 2) NOT NULL,
	[AccruedInterest] [numeric](17, 2) NOT NULL,
	[AccruedInterestBankPortion] [numeric](17, 2) NULL,
	[LCInstallmentAmountDisc] [numeric](17, 2) NOT NULL,
	[LCInsuranceAmountDisc] [numeric](17, 2) NOT NULL,
	[InsuranceAmountDisc] [numeric](17, 2) NOT NULL,
	[InstallmentAmountDisc] [numeric](17, 2) NOT NULL,
	[PrepaymentFeeDisc] [numeric](17, 2) NOT NULL,
	[PrepaidAmount] [numeric](17, 2) NOT NULL,
	[InsuranceTerminationFlag] [char](1) NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
	[ReasonTypeID] [char](5) NOT NULL,
	[ReasonID] [varchar](10) NOT NULL,
	[JournalNo] [nvarchar](20) NOT NULL,
	[PrepaymentStatus] [char](1) NOT NULL,
	[VoucherNo] [char](20) NOT NULL,
	[StatusDate] [datetime] NOT NULL,
	[RequestBy] [nvarchar](20) NOT NULL,
	[ApprovalNo] [nvarchar](50) NOT NULL,
	[AccruedInterestDisc] [numeric](17, 2) NULL,
	[OSPrincipalDue] [numeric](17, 2) NULL,
	[OSPrincipalDueBank] [numeric](17, 2) NULL,
	[OSInterestDue] [numeric](17, 2) NULL,
	[OSInterestDueBank] [numeric](17, 2) NULL,
	[PrincipalDueDisc] [numeric](17, 2) NULL,
	[InterestDueDisc] [numeric](17, 2) NULL,
	[ToleranceAmount] [numeric](17, 2) NULL,
	[OSDiffRate] [numeric](17, 2) NULL,
	[AccruedDiffRateEOM] [numeric](17, 2) NULL,
	[OSInsuranceIncome] [numeric](17, 2) NULL,
	[AccruedInsuranceIncomeEOM] [numeric](17, 2) NULL,
	[OSIncentive] [numeric](17, 2) NULL,
	[AccruedIncentiveEOM] [numeric](17, 2) NULL,
	[OSProvision] [numeric](17, 2) NULL,
	[AccruedProvisionEOM] [numeric](17, 2) NULL,
	[OSAdminFee] [numeric](17, 2) NOT NULL,
	[AccruedAdminFeeEOM] [numeric](17, 2) NOT NULL,
	[OSDeferredInsurInc] [numeric](17, 2) NULL,
	[AccruedDeferredInsurIncEOM] [numeric](17, 2) NULL,
	[OSOtherRefund] [numeric](17, 2) NULL,
	[AccruedOtherRefundEOM] [numeric](17, 2) NULL,
	[OSAdmFee] [numeric](17, 2) NULL,
	[AccruedAdmFeeEOM] [numeric](17, 2) NULL,
	[OSProvisionFee] [numeric](17, 2) NULL,
	[AccruedProvisionFeeEOM] [numeric](17, 2) NULL,
	[OSOtherFee] [numeric](17, 2) NULL,
	[AccruedOtherFeeEOM] [numeric](17, 2) NULL,
	[OSSurveyFee] [numeric](17, 2) NULL,
	[AccruedSurveyFeeEOM] [numeric](17, 2) NULL,
	[VisitFeeCharges] [numeric](17, 2) NULL,
	[VisitFeePaid] [numeric](17, 2) NULL,
	[VisitFeeDisc] [numeric](17, 2) NULL,
	[PickUpFeeCharges] [numeric](17, 2) NULL,
	[PickUpFeePaid] [numeric](17, 2) NULL,
	[PickUpFeeDisc] [numeric](17, 2) NULL,
	[OSVisitFee] [numeric](17, 2) NULL,
	[OSPickUpFee] [numeric](17, 2) NULL,
	[UsrUpd] [nvarchar](20) NOT NULL,
	[DtmUpd] [datetime] NOT NULL,
	[UsrCrt] [nvarchar](20) NULL,
	[DtmCrt] [datetime] NULL,
 CONSTRAINT [PK_EarlyTermination] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING ON
GO
ALTER TABLE [dbo].[EarlyTermination] ADD  CONSTRAINT [DF_EarlyTermination_OSAdminFee]  DEFAULT ((0)) FOR [OSAdminFee]
GO
ALTER TABLE [dbo].[EarlyTermination] ADD  CONSTRAINT [DF_EarlyTermination_AccruedAdminFeeEOM]  DEFAULT ((0)) FOR [AccruedAdminFeeEOM]
GO
ALTER TABLE [dbo].[EarlyTermination] ADD  CONSTRAINT [DF_EarlyTermination_UsrCrt]  DEFAULT (suser_sname()) FOR [UsrCrt]
GO
ALTER TABLE [dbo].[EarlyTermination] ADD  CONSTRAINT [DF_EarlyTermination_DtmCrt]  DEFAULT (getdate()) FOR [DtmCrt]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Total O/S AR saat di capture' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EarlyTermination', @level2type=N'COLUMN',@level2name=N'PrepaymentAmount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'''C'' - Cabut Klausal Leasing, ''T'' - Termination' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EarlyTermination', @level2type=N'COLUMN',@level2name=N'InsuranceTerminationFlag'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Status (''R''-Request,''A''-Approve,''C''-Cancel,''X''-Expire,''J''-Reject,''E''-Execute)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'EarlyTermination', @level2type=N'COLUMN',@level2name=N'PrepaymentStatus'
GO
