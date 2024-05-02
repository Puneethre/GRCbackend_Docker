﻿-- DROP SCHEMA master_config;

CREATE SCHEMA master_config AUTHORIZATION "GRC";
-- master_config."ACTIVITIY_NAME_MASTER" definition

-- Drop table

-- DROP TABLE master_config."ACTIVITIY_NAME_MASTER";

CREATE TABLE master_config."ACTIVITIY_NAME_MASTER" (
	"ID" int4 NOT NULL,
	"ACTIVITY_NAME" varchar(50) NOT NULL,
	CONSTRAINT "ACTIVITIY_NAME_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."ACTIVITIY_NAME_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."ACTIVITIY_NAME_MASTER" TO "GRC";


-- master_config."CATEGORY_MASTER" definition

-- Drop table

-- DROP TABLE master_config."CATEGORY_MASTER";

CREATE TABLE master_config."CATEGORY_MASTER" (
	"CATEGORY_ID" int4 NOT NULL,
	"CATEGORY" varchar(50) NOT NULL,
	"IS_ACTIVE" bool DEFAULT true NOT NULL,
	CONSTRAINT "CATEGORY_MASTER_pkey" PRIMARY KEY ("CATEGORY_ID")
);

-- Permissions

ALTER TABLE master_config."CATEGORY_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."CATEGORY_MASTER" TO "GRC";


-- master_config."DOCUMENT_MASTER" definition

-- Drop table

-- DROP TABLE master_config."DOCUMENT_MASTER";

CREATE TABLE master_config."DOCUMENT_MASTER" (
	"ID" int4 NOT NULL,
	"DOCUMENT_NAME" varchar(100) NOT NULL,
	CONSTRAINT "DOCUMENT_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."DOCUMENT_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."DOCUMENT_MASTER" TO "GRC";


-- master_config."DOMAIN_MASTER" definition

-- Drop table

-- DROP TABLE master_config."DOMAIN_MASTER";

CREATE TABLE master_config."DOMAIN_MASTER" (
	"ID" int4 NOT NULL,
	"DOMAIN_NAME" varchar(50) NOT NULL,
	CONSTRAINT "DOMAIN_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."DOMAIN_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."DOMAIN_MASTER" TO "GRC";


-- master_config."ENTITLEMENT_MASTER" definition

-- Drop table

-- DROP TABLE master_config."ENTITLEMENT_MASTER";

CREATE TABLE master_config."ENTITLEMENT_MASTER" (
	"ROLE_ID" int4 NOT NULL,
	"MENU_ITEM" varchar(50) NOT NULL,
	"IS_ACTIVE" bool DEFAULT true NOT NULL,
	CONSTRAINT "ENTITLEMENT_MASTER_pkey" PRIMARY KEY ("ROLE_ID")
);

-- Permissions

ALTER TABLE master_config."ENTITLEMENT_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."ENTITLEMENT_MASTER" TO "GRC";


-- master_config."FREQUENCY_MASTER" definition

-- Drop table

-- DROP TABLE master_config."FREQUENCY_MASTER";

CREATE TABLE master_config."FREQUENCY_MASTER" (
	"ID" int4 NOT NULL,
	"FREQUENCY" varchar(20) NOT NULL,
	CONSTRAINT "FREQUENCY_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."FREQUENCY_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."FREQUENCY_MASTER" TO "GRC";


-- master_config."GOVERNANCE_MASTER" definition

-- Drop table

-- DROP TABLE master_config."GOVERNANCE_MASTER";

CREATE TABLE master_config."GOVERNANCE_MASTER" (
	"ID" int4 NOT NULL,
	"NAME" varchar(50) NOT NULL,
	"SHORT_NAME" varchar(10) NULL,
	"IS_ACTIVE" bool DEFAULT true NOT NULL,
	CONSTRAINT "GOVERNANCE_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."GOVERNANCE_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."GOVERNANCE_MASTER" TO "GRC";


-- master_config."PROCESS_MASTER" definition

-- Drop table

-- DROP TABLE master_config."PROCESS_MASTER";

CREATE TABLE master_config."PROCESS_MASTER" (
	"ID" int4 NOT NULL,
	"PROCESS_NAME" varchar(50) NOT NULL,
	CONSTRAINT "PROCESS_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."PROCESS_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."PROCESS_MASTER" TO "GRC";


-- master_config."ROLE_TYPE" definition

-- Drop table

-- DROP TABLE master_config."ROLE_TYPE";

CREATE TABLE master_config."ROLE_TYPE" (
	"ID" int4 NOT NULL,
	"ROLE_TYPE_DESC" varchar(30) NOT NULL,
	CONSTRAINT "ROLE_TYPE_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."ROLE_TYPE" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."ROLE_TYPE" TO "GRC";


-- master_config."STATUS_MASTER" definition

-- Drop table

-- DROP TABLE master_config."STATUS_MASTER";

CREATE TABLE master_config."STATUS_MASTER" (
	"ID" int4 NOT NULL,
	"STATUS" varchar(20) NOT NULL,
	CONSTRAINT "STATUS_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."STATUS_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."STATUS_MASTER" TO "GRC";


-- master_config."TECHNOLOGIES_MASTER" definition

-- Drop table

-- DROP TABLE master_config."TECHNOLOGIES_MASTER";

CREATE TABLE master_config."TECHNOLOGIES_MASTER" (
	"ID" int4 NOT NULL,
	"TECHNOLOGY_NAME" varchar(50) NOT NULL,
	CONSTRAINT "TECHNOLOGIES_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."TECHNOLOGIES_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."TECHNOLOGIES_MASTER" TO "GRC";


-- master_config."CATEGORY_LIST_MASTER" definition

-- Drop table

-- DROP TABLE master_config."CATEGORY_LIST_MASTER";

CREATE TABLE master_config."CATEGORY_LIST_MASTER" (
	"LIST_ID" int4 NOT NULL,
	"CATEGORY_ID" int4 NOT NULL,
	"CATEGORY_NAME" varchar(50) NOT NULL,
	"DESCRIPTION" text NULL,
	CONSTRAINT "CATEGORY_LIST_MASTER_pkey" PRIMARY KEY ("LIST_ID"),
	CONSTRAINT "CATEGORY_ID_FK" FOREIGN KEY ("CATEGORY_ID") REFERENCES master_config."CATEGORY_MASTER"("CATEGORY_ID")
);

-- Permissions

ALTER TABLE master_config."CATEGORY_LIST_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."CATEGORY_LIST_MASTER" TO "GRC";


-- master_config."STANDARD_MASTER" definition

-- Drop table

-- DROP TABLE master_config."STANDARD_MASTER";

CREATE TABLE master_config."STANDARD_MASTER" (
	"ID" int4 NOT NULL,
	"SHORT_NAME" varchar(10) NULL,
	"NAME" varchar(50) NOT NULL,
	"GOVR_ID" int4 NOT NULL,
	"LEVELS" int4 NOT NULL,
	"LEVEL_NAMES" varchar(100) NOT NULL,
	"NO_OF_CONTROLS" int4 NULL,
	"IS_ACTIVE" bool DEFAULT true NOT NULL,
	CONSTRAINT "STANDARD_MASTER_pkey" PRIMARY KEY ("ID"),
	CONSTRAINT "GOVR_ID_FK" FOREIGN KEY ("GOVR_ID") REFERENCES master_config."GOVERNANCE_MASTER"("ID")
);

-- Permissions

ALTER TABLE master_config."STANDARD_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."STANDARD_MASTER" TO "GRC";


-- master_config."COMPLIANCE_MASTER" definition

-- Drop table

-- DROP TABLE master_config."COMPLIANCE_MASTER";

CREATE TABLE master_config."COMPLIANCE_MASTER" (
	"ID" int4 NOT NULL,
	"STANDARD_ID" int4 NOT NULL,
	"GOVERNANCE_ID" int4 NOT NULL,
	"COMPL_START_DATE" date NOT NULL,
	"COMPL_END_DATE" date NOT NULL,
	"MET_COMPLIANCE" bool DEFAULT true NOT NULL,
	"IS_ACTIVE" bool DEFAULT true NOT NULL,
	CONSTRAINT "COMPLIANCE_MASTER_pkey" PRIMARY KEY ("ID"),
	CONSTRAINT "GOVERNANCE_ID_FK" FOREIGN KEY ("GOVERNANCE_ID") REFERENCES master_config."GOVERNANCE_MASTER"("ID"),
	CONSTRAINT "STANDARD_ID_fk" FOREIGN KEY ("STANDARD_ID") REFERENCES master_config."STANDARD_MASTER"("ID")
);

-- Permissions

ALTER TABLE master_config."COMPLIANCE_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."COMPLIANCE_MASTER" TO "GRC";


-- master_config."ACTIVITY_MASTER" definition

-- Drop table

-- DROP TABLE master_config."ACTIVITY_MASTER";

CREATE TABLE master_config."ACTIVITY_MASTER" (
	"ID" int4 NOT NULL,
	"ACTIVITY_DESCR" text NULL,
	"DOER_ROLE" int4 NOT NULL,
	"FREQUENCY_ID" int4 NOT NULL,
	"DURATION" int4 NULL,
	"REF_DOCUMENT_ID" int4 NULL,
	"OUTPUT_DOCUMENT_PATH" int4 NULL,
	"TRIGGERING_ACTIVITY_NAME_ID" int4 NULL,
	"APPROVER_ROLE" int4 NULL,
	"PRACTICE_ID" int4 NULL,
	"HELP_REF" int4 NULL,
	"IS_ACTIVE" bool NOT NULL,
	"AUDITABLE" bool NOT NULL,
	"ACTIVITY_NAME_ID" int4 NULL,
	CONSTRAINT "ACTIVITY_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."ACTIVITY_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."ACTIVITY_MASTER" TO "GRC";


-- master_config."ASSIGNMENT_MASTER" definition

-- Drop table

-- DROP TABLE master_config."ASSIGNMENT_MASTER";

CREATE TABLE master_config."ASSIGNMENT_MASTER" (
	"ID" int4 NOT NULL,
	"ACTIVITY_MASTER_ID" int4 NOT NULL,
	"DOER_CLI_USER_ID" int4 NOT NULL,
	"START_DATE" date NOT NULL,
	"END_DATE" date NOT NULL,
	"APPROVAL_DATE" timestamptz NULL,
	"APPROVER_CLI_USER_ID" int4 NULL,
	"DOER_COMMENTS" varchar(255) NULL,
	"APPROVER_COMMENTS" varchar(255) NULL,
	"EVIDENCE_DETAILS" varchar(255) NULL,
	"AUDIT_CHECK" bool DEFAULT false NOT NULL,
	"APPROVAL_STATUS" bool NOT NULL,
	"DOER_STATUS" int4 DEFAULT 0 NOT NULL,
	CONSTRAINT "ASSIGNMENT_MASTER_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."ASSIGNMENT_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."ASSIGNMENT_MASTER" TO "GRC";


-- master_config."CLIENT_ROLE_MASTER" definition

-- Drop table

-- DROP TABLE master_config."CLIENT_ROLE_MASTER";

CREATE TABLE master_config."CLIENT_ROLE_MASTER" (
	"CLI_ROLE_ID" int4 NOT NULL,
	"ROLE_NAME" varchar(50) NOT NULL,
	"ROLE_TYPE_ID" int4 NULL,
	"DESCRIPTION" text NULL,
	"COMMENTS" text NULL,
	"CREATED_BY" int4 NULL,
	"is_ACTIVE" bool DEFAULT true NOT NULL,
	"CREATED_DATE_TIME" timestamptz NOT NULL,
	CONSTRAINT "CLIENT_ROLE_MASTER_pkey" PRIMARY KEY ("CLI_ROLE_ID")
);

-- Permissions

ALTER TABLE master_config."CLIENT_ROLE_MASTER" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."CLIENT_ROLE_MASTER" TO "GRC";


-- master_config."CLIENT_USER_INFO" definition

-- Drop table

-- DROP TABLE master_config."CLIENT_USER_INFO";

CREATE TABLE master_config."CLIENT_USER_INFO" (
	"CLI_USER_ID" int4 NOT NULL,
	"NAME" varchar(100) NOT NULL,
	"EMAIL" varchar(120) NOT NULL,
	"CUSTOMER_ID" int4 NOT NULL,
	"SYS_USER_ID" int4 NOT NULL,
	"CLI_ROLE_ID" int4 NOT NULL,
	"CREATED_BY" int4 NOT NULL,
	"IS_ACTIVE" bool DEFAULT true NOT NULL,
	"CREATED_DATE_TIME" timestamptz NOT NULL,
	CONSTRAINT "CLIENT_USER_INFO_pkey" PRIMARY KEY ("CLI_USER_ID")
);

-- Permissions

ALTER TABLE master_config."CLIENT_USER_INFO" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."CLIENT_USER_INFO" TO "GRC";


-- master_config."USER_ACTIVITY_EMAIL" definition

-- Drop table

-- DROP TABLE master_config."USER_ACTIVITY_EMAIL";

CREATE TABLE master_config."USER_ACTIVITY_EMAIL" (
	"ID" int4 NOT NULL,
	"ACTIVITY_ID" int4 NOT NULL,
	"EMAIL_CODE_TO_ACTIVITY" varchar(30) NOT NULL,
	CONSTRAINT "USER_ACTIVITY_EMAIL_pkey" PRIMARY KEY ("ID")
);

-- Permissions

ALTER TABLE master_config."USER_ACTIVITY_EMAIL" OWNER TO "GRC";
GRANT ALL ON TABLE master_config."USER_ACTIVITY_EMAIL" TO "GRC";


-- master_config."ACTIVITY_MASTER" foreign keys

ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "ACTIVITY_NAME_ID_FK" FOREIGN KEY ("ACTIVITY_NAME_ID") REFERENCES master_config."ACTIVITIY_NAME_MASTER"("ID");
ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "APPROVER_ROLE_FK" FOREIGN KEY ("APPROVER_ROLE") REFERENCES master_config."CLIENT_ROLE_MASTER"("CLI_ROLE_ID");
ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "DOER_ROLE_FK" FOREIGN KEY ("DOER_ROLE") REFERENCES master_config."CLIENT_ROLE_MASTER"("CLI_ROLE_ID");
ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "FREQUENCY_FK" FOREIGN KEY ("FREQUENCY_ID") REFERENCES master_config."FREQUENCY_MASTER"("ID");
ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "OUTPUT_DOCUMENT_FK" FOREIGN KEY ("OUTPUT_DOCUMENT_PATH") REFERENCES master_config."DOCUMENT_MASTER"("ID");
ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "REF_DOCUMENT_FK" FOREIGN KEY ("REF_DOCUMENT_ID") REFERENCES master_config."DOCUMENT_MASTER"("ID");
ALTER TABLE master_config."ACTIVITY_MASTER" ADD CONSTRAINT "TRIGGERING_ACTIVITY_FK" FOREIGN KEY ("TRIGGERING_ACTIVITY_NAME_ID") REFERENCES master_config."ACTIVITIY_NAME_MASTER"("ID");


-- master_config."ASSIGNMENT_MASTER" foreign keys

ALTER TABLE master_config."ASSIGNMENT_MASTER" ADD CONSTRAINT "ACTIVITY_ID_FK" FOREIGN KEY ("ACTIVITY_MASTER_ID") REFERENCES master_config."ACTIVITY_MASTER"("ID");
ALTER TABLE master_config."ASSIGNMENT_MASTER" ADD CONSTRAINT "APPROVER_CLI_USER_ID_FK" FOREIGN KEY ("APPROVER_CLI_USER_ID") REFERENCES master_config."CLIENT_USER_INFO"("CLI_USER_ID");
ALTER TABLE master_config."ASSIGNMENT_MASTER" ADD CONSTRAINT "DOER_CLI_USER_ID_FK" FOREIGN KEY ("DOER_CLI_USER_ID") REFERENCES master_config."CLIENT_USER_INFO"("CLI_USER_ID");


-- master_config."CLIENT_ROLE_MASTER" foreign keys

ALTER TABLE master_config."CLIENT_ROLE_MASTER" ADD CONSTRAINT "CREATED_BY_FK" FOREIGN KEY ("CREATED_BY") REFERENCES system_info."SYS_USER_LOGIN"("SYS_USER_ID");
ALTER TABLE master_config."CLIENT_ROLE_MASTER" ADD CONSTRAINT "ROLE_TYPE_ID_FK" FOREIGN KEY ("ROLE_TYPE_ID") REFERENCES master_config."ROLE_TYPE"("ID");


-- master_config."CLIENT_USER_INFO" foreign keys

ALTER TABLE master_config."CLIENT_USER_INFO" ADD CONSTRAINT "CLI_ROLE_ID_FK" FOREIGN KEY ("CLI_ROLE_ID") REFERENCES master_config."CLIENT_ROLE_MASTER"("CLI_ROLE_ID");
ALTER TABLE master_config."CLIENT_USER_INFO" ADD CONSTRAINT "CREATED_BY_FK" FOREIGN KEY ("CREATED_BY") REFERENCES system_info."SYS_USER_LOGIN"("SYS_USER_ID");
ALTER TABLE master_config."CLIENT_USER_INFO" ADD CONSTRAINT "CUSTOMER_ID_FK" FOREIGN KEY ("CUSTOMER_ID") REFERENCES system_info."SYS_CUSTOMER_INFO"("CUSTOMER_ID");
ALTER TABLE master_config."CLIENT_USER_INFO" ADD CONSTRAINT "SYS_USER_ID_FK" FOREIGN KEY ("SYS_USER_ID") REFERENCES system_info."SYS_USER_LOGIN"("SYS_USER_ID");


-- master_config."USER_ACTIVITY_EMAIL" foreign keys

ALTER TABLE master_config."USER_ACTIVITY_EMAIL" ADD CONSTRAINT "ACTIVITY_ID_FK" FOREIGN KEY ("ACTIVITY_ID") REFERENCES master_config."ACTIVITY_MASTER"("ID");




-- Permissions

GRANT ALL ON SCHEMA master_config TO "GRC";