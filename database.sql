ALTER TABLE "public"."owa_project_person" DROP CONSTRAINT "FK_owa_project_person_project_id";
ALTER TABLE "public"."owa_project_person" DROP CONSTRAINT "FK_owa_person_project_person_id";
DROP TABLE IF EXISTS "public"."owa_project";
DROP TABLE IF EXISTS "public"."owa_person";
DROP TABLE IF EXISTS "public"."owa_project_person";
CREATE TABLE "public"."owa_project" ( 
  "id" SERIAL,
  "project_name" VARCHAR(50) NOT NULL,
  CONSTRAINT "owa_project_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."owa_person" ( 
  "id" SERIAL,
  "person_name" VARCHAR(25) NOT NULL,
  CONSTRAINT "owa_person_pkey" PRIMARY KEY ("id")
);
CREATE TABLE "public"."owa_project_person" ( 
  "id" SERIAL,
  "project_id" INTEGER NOT NULL,
  "person_id" INTEGER NOT NULL,
  "hours" INTEGER NOT NULL,
  CONSTRAINT "owa_project_person_pkey" PRIMARY KEY ("id")
);
ALTER TABLE "public"."owa_project_person" ADD CONSTRAINT "FK_owa_project_person_project_id" FOREIGN KEY ("project_id") REFERENCES "public"."owa_project" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."owa_project_person" ADD CONSTRAINT "FK_owa_person_project_person_id" FOREIGN KEY ("person_id") REFERENCES "public"."owa_person" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;