# movie-ticket-backend
Backend of movie ticket with C# to integrative work of the 4th semester

<br />

# Data Sheet

MOVIE

| id | PK |
| --- | --- |
| name | text |
| synopsis | text |
| director | text |
| banner_url | text |
<br />
SESSION

| id | PK |
| --- | --- |
| datetime | timestamp |
| room | int |
| movie_id | FK |
<br />
USER_TYPE

| id | PK |
| --- | --- |
| name | “admin” | “user” |
<br />
USER

| id | PK |
| --- | --- |
| email | string |
| password | string |
| user_type_id | FK |
<br />
TICKET

| id | PK |
| --- | --- |
| user_id | FK |
| session_id | FK |
| chair_coord | string |
