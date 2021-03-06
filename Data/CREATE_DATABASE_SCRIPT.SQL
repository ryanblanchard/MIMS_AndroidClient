/***************************************** 
 * CREATE DATABASE SCRIPT *
 * DATE CREATED : 2015-13-09
 * AUTHOR 	: RYAN
 ****************************************/

CREATE TABLE "MIMS_MATERIAL_SRC" (
  [ID] NUMBER NOT NULL, 
  [MATERIAL_SRC_NO] VARCHAR2(50) NOT NULL, 
  [BORROW_PIT_NO] VARCHAR2(50), 
  [ROAD_NO] VARCHAR2(7) NOT NULL, 
  [KILOMETRE_POSITION] NUMBER(6,2) NOT NULL, 
  [OFFSET_LEFT_RIGHT] VARCHAR2(2) NOT NULL, 
  [OFFSET_IN_METERS] NUMBER(6,2) NOT NULL, 
  [DEGREES_DECIMAL_X] NUMBER(15,8) NOT NULL, 
  [DEGREES_DECIMAL_Y] NUMBER(15,8) NOT NULL, 
  CONSTRAINT [] PRIMARY KEY ([ID]));


CREATE TABLE [MIMS_UPLOADED_PHOTOS] (
  [ID] INT, 
  [BORROW_PIT_ID] INT(50), 
  [USERNAME] VARCHAR2(50), 
  [TRANSACTION_DATE] DATE, 
  [PHOTO_DATA] BLOB(5000), 
  [PHOTO_FILENAME] VARCHAR2(255), 
  [UPLOADED] CHAR DEFAULT 'N', 
  [DEGREES_DECIMAL_X] NUMBER(15, 8), 
  [DEGREES_DECIMAL_Y] NUMBER(15, 8));


CREATE TABLE MIMS_UPL_PHOTO_CATEGORIES

(

  ID                    NUMBER                  NOT NULL,

  PHOTO_CATEGORY_DESCR  VARCHAR2(250)      NOT NULL,

  CREATED_BY            VARCHAR2(30)       NOT NULL,

  CREATED_DATE          DATETIME                    NOT NULL,

  MODIFIED_BY           VARCHAR2(30),

  MODIFIED_DATE         DATETIME,

  SORT_ORDER            NUMBER

);


