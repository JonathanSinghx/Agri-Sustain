CREATE TABLE [dbo].[Table] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [product_name] VARCHAR (50) NULL,
    [price]        FLOAT (53)   NULL,
    [quantity]     INT          NULL,
    [image]        IMAGE        NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[Order] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [o_date]    DATETIME      NULL,
    [cust_name] VARCHAR (50)  NULL,
    [cust_addr] VARCHAR (100) NULL,
    [cust_em]   VARCHAR (35)  NULL,
    [data]      VARCHAR (MAX) NULL,
    [card_type] VARCHAR (15)  NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
CREATE TABLE [dbo].[R_Table] (
    [Id]        INT           NOT NULL,
    [rev]       VARCHAR (150) NULL,
    [r_amt]     INT           NULL,
    [prod_name] VARCHAR (50)  NULL,
    [dateTime]  DATETIME      NULL
);