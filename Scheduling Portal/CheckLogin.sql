-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
-- =============================================
-- Author:		Tony
-- Create date: 
-- Description:	
-- =============================================
CREATE PROCEDURE CheckLogin
	-- Add the parameters for the stored procedure here
	@strLoginNameIn varchar
	,@strPasswordIn varchar

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT UserID, UserTypeID
	FROM dbo.UserData
	where LoginName = @strLoginNameIn AND [Password] = @strPasswordIn

END
GO
