CREATE DATABASE "lucifer";

\connect "lucifer";

create table "Products"
(
    "Id" uuid not null constraint "PK_Products" primary key,
    "Name" text,
    "Description" text not null,
    "Enabled" boolean not null
);