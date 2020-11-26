/****** Script for SelectTopNRows command from SSMS  ******/
--truncate table [OrderProducts].[dbo].[orders]
--delete from [OrderProducts].[dbo].[orders] where converted_order is null
SELECT *
,JSON_VALUE(source_order,'$.products[2].paidPrice') as source_order_paidPrice
--,JSON_VALUE(converted_order,'$.products[2].paidPrice') as converted_order_paidPrice
,JSON_VALUE(converted_order,'$.products[2].quantity') as quantity
  FROM [OrderProducts].[dbo].[orders]
order by id desc

