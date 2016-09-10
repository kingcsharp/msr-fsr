CREATE TABLE [dbo].[A_PART_REPLACEMENT_DATA] (
    [ID]              VARCHAR (50) NOT NULL,
    [IN_OUT]          CHAR (10)    NOT NULL,
    [TASK_ID]         VARCHAR (50) NOT NULL,
    [DESTINATION]     VARCHAR (50) NULL,
    [PO]              VARCHAR (50) NULL,
    [MACHINE_PART_ID] VARCHAR (50) NULL,
    [NCDR]            VARCHAR (50) NULL,
    [TRACKING]        VARCHAR (50) NULL,
    [SO]              CHAR (10)    NULL,
    [DTTM]            DATETIME     NULL,
    [SHIP_PRIORITY]   VARCHAR (50) NULL,
    [SHIP_BY]         VARCHAR (50) NULL,
    [SHIPMENT_AUTH]   BIT          NULL,
    [PAYMENT_AUTH]    BIT          NULL,
    [DRCM]            DATETIME     NULL,
    [MODBY]           VARCHAR (50) NULL,
    CONSTRAINT [PK_A_PART_REPLACEMENT_DATA] PRIMARY KEY CLUSTERED ([ID] ASC)
);

