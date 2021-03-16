# NoBalloons-Public
A satirical news site.

- For demo, see bottom.

**NB: The web.config file has been deleted for security purposes. This also means there is no database attached.**

These were the tables used in the database.

 1.  Article
 
    - [Title]       VARCHAR (MAX) NOT NULL,
    - [Author]      VARCHAR (MAX) NOT NULL,
    - [Content]     VARCHAR (MAX) NOT NULL,
    - [ArticleDate] DATE          NOT NULL,
    - [Image]       NVARCHAR (50) NOT NULL,
    - [Summary]     VARCHAR (MAX) NOT NULL,
    - [Main]        CHAR (1)      NOT NULL,
    - [Link]        NVARCHAR (50) NOT NULL,
    - CONSTRAINT [PK_Article] PRIMARY KEY CLUSTERED ([Link] ASC)
         
 2.  Horoscope
 
    - [Sign]    NVARCHAR (50) NOT NULL,
    - [Message] VARCHAR (MAX) NOT NULL,
    - PRIMARY KEY CLUSTERED ([Sign] ASC)
         
         
   - Video showing home and article view.      
   https://user-images.githubusercontent.com/60546240/111319688-f29f9000-866e-11eb-9882-75baa3c03fd3.mp4
   
   - Video showing horoscopes and about view.    
   https://user-images.githubusercontent.com/60546240/111319904-24185b80-866f-11eb-91c6-5cd1505cfc48.mp4
   
   - Video showing search and search results.    
    https://user-images.githubusercontent.com/60546240/111320321-896c4c80-866f-11eb-8808-41bcb773bf47.mp4
