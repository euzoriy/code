Option Strict Off' N o t   f i n i s h e d  
 '   N X   1 1 . 0 . 1 . 1 1  
 '   J o u r n a l   c r e a t e d   b y   i e v g e n z   o n   M o n   M a y   0 8   0 8 : 5 4 : 4 5   2 0 1 7   E a s t e r n   D a y l i g h t   T i m e  
 '  
 I m p o r t s   S y s t e m  
 I m p o r t s   N X O p e n  
 M o d u l e   N X J o u r n a l  
 S u b   M a i n   ( B y V a l   a r g s ( )   A s   S t r i n g )    
 D i m   t h e S e s s i o n   A s   N X O p e n . S e s s i o n   =   N X O p e n . S e s s i o n . G e t S e s s i o n ( )  
 D i m   w o r k P a r t   A s   N X O p e n . P a r t   =   t h e S e s s i o n . P a r t s . W o r k  
 D i m   d i s p l a y P a r t   A s   N X O p e n . P a r t   =   t h e S e s s i o n . P a r t s . D i s p l a y  
 '   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
 '       M e n u :   F i l e - > & E d i t . . .  
 '   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
 D i m   m a r k I d 1   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 m a r k I d 1   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . V i s i b l e ,   " S t a r t " )  
 D i m   n o t e 1   A s   N X O p e n . A n n o t a t i o n s . N o t e   =   C T y p e ( w o r k P a r t . F i n d O b j e c t ( " H A N D L E   R - 1 6 3 5 0 8 7 " ) ,   N X O p e n . A n n o t a t i o n s . N o t e )  
 D i m   d r a f t i n g N o t e B u i l d e r 1   A s   N X O p e n . A n n o t a t i o n s . D r a f t i n g N o t e B u i l d e r   =   N o t h i n g  
 d r a f t i n g N o t e B u i l d e r 1   =   w o r k P a r t . A n n o t a t i o n s . C r e a t e D r a f t i n g N o t e B u i l d e r ( n o t e 1 )  
 d r a f t i n g N o t e B u i l d e r 1 . T e x t . T e x t B l o c k . C u s t o m S y m b o l S c a l e   =   1 . 0  
 d r a f t i n g N o t e B u i l d e r 1 . O r i g i n . S e t I n f e r R e l a t i v e T o G e o m e t r y ( T r u e )  
 t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 1 ,   " N o t e   D i a l o g " )  
 d r a f t i n g N o t e B u i l d e r 1 . O r i g i n . S e t I n f e r R e l a t i v e T o G e o m e t r y ( T r u e )  
 D i m   l e a d e r D a t a 1   A s   N X O p e n . A n n o t a t i o n s . L e a d e r D a t a   =   N o t h i n g  
 l e a d e r D a t a 1   =   w o r k P a r t . A n n o t a t i o n s . C r e a t e L e a d e r D a t a ( )  
 l e a d e r D a t a 1 . S t u b S i z e   =   0 . 1 2 5  
 l e a d e r D a t a 1 . A r r o w h e a d   =   N X O p e n . A n n o t a t i o n s . L e a d e r D a t a . A r r o w h e a d T y p e . F i l l e d A r r o w  
 l e a d e r D a t a 1 . V e r t i c a l A t t a c h m e n t   =   N X O p e n . A n n o t a t i o n s . L e a d e r V e r t i c a l A t t a c h m e n t . C e n t e r  
 d r a f t i n g N o t e B u i l d e r 1 . L e a d e r . L e a d e r s . A p p e n d ( l e a d e r D a t a 1 )  
 l e a d e r D a t a 1 . S t u b S i d e   =   N X O p e n . A n n o t a t i o n s . L e a d e r S i d e . I n f e r r e d  
 D i m   s y m b o l s c a l e 1   A s   D o u b l e   =   N o t h i n g  
 s y m b o l s c a l e 1   =   d r a f t i n g N o t e B u i l d e r 1 . T e x t . T e x t B l o c k . S y m b o l S c a l e  
 D i m   s y m b o l a s p e c t r a t i o 1   A s   D o u b l e   =   N o t h i n g  
 s y m b o l a s p e c t r a t i o 1   =   d r a f t i n g N o t e B u i l d e r 1 . T e x t . T e x t B l o c k . S y m b o l A s p e c t R a t i o  
 d r a f t i n g N o t e B u i l d e r 1 . O r i g i n . S e t I n f e r R e l a t i v e T o G e o m e t r y ( T r u e )  
 d r a f t i n g N o t e B u i l d e r 1 . O r i g i n . S e t I n f e r R e l a t i v e T o G e o m e t r y ( T r u e )  
 ' D i m   m a r k I d 2   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 ' m a r k I d 2   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " N o t e " )  
 ' t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 2 ,   N o t h i n g )  
 ' D i m   m a r k I d 3   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 ' m a r k I d 3   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " N o t e " )  
 ' t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 3 ,   N o t h i n g )  
 	 D i m   m a r k I d 4   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 	 m a r k I d 4   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " N o t e " )  
 	 	 D i m   t e x t 1 ( 1 3 )   A s   S t r i n g  
 	 	 t e x t 1 ( 0 )   =   " N O T E S : "  
 	 	 t e x t 1 ( 1 )   =   " "  
 	 	 t e x t 1 ( 2 )   =   "   1 .   A L L   D I M E N S I O N S   A R E   I N   I N C H E S   U O S "  
 	 	 t e x t 1 ( 3 )   =   "   2 .   C O R N E R S   M U S T   H A V E   F I L L E T S   . 0 0 5   -   . 0 2 0   U O S "  
 	 	 t e x t 1 ( 4 )   =   "   3 .   B R E A K   S H A R P   E D G E S   . 0 0 3   -   . 0 1 5   U O S "  
 	 	 t e x t 1 ( 5 )   =   "   4 .   C H A M F E R S   A R E   . 0 2 0   -   . 0 4 0   U O S "  
 	 	 t e x t 1 ( 6 )   =   "   5 .   D W G   I N T E R P R E T A T I O N S   P E R   A S M E   Y 1 4 . 5 - 2 0 0 9 "  
 	 	 t e x t 1 ( 7 )   =   "   6 .   U O S   A N G U L A R   T O L E R A N C E S   A R E :   < $ t > 5 < $ s > "  
 	 	 t e x t 1 ( 8 )   =   "   7 .   U O S   L I N E A R   T O L E R A N C E S   A R E : . X X X   < $ t > . 0 0 5 , . X X   < $ t > . 0 1 "  
 	 	 t e x t 1 ( 9 )   =   "   8 .   A L L   M A C H I N E D   S U R F A C E S   A R E   1 2 5   R M S   U O S "  
 	 	 t e x t 1 ( 1 0 )   =   "   9 .   A S S O C I A T E D   C O M P U T E R   D A T A   F I L E S   A R E :   "  
 	 	 t e x t 1 ( 1 1 )   =   "                       N X   D W G                 < W @ $ S H _ P A R T _ N A M E > . P R T "  
 	 	 t e x t 1 ( 1 2 )   =   "                       N X   M O D E L             < W @ $ S H _ M A S T E R _ P A R T _ N A M E > . P R T .   "  
 	 	 t e x t 1 ( 1 3 )   =   " 1 0 .   U O S   A L L   < O >   < & 7 0 > < + > < & 1 0 > < + > < O > . 0 1 0 < + > A < + > B < + > C < + > < & 9 0 > "  
 	 	 d r a f t i n g N o t e B u i l d e r 1 . T e x t . T e x t B l o c k . S e t T e x t ( t e x t 1 )  
 	 	 D i m   t e x t 2 ( 1 4 )   A s   S t r i n g  
 	 	 t e x t 2 ( 0 )   =   " N O T E S : "  
 	 	 t e x t 2 ( 1 )   =   " "  
 	 	 t e x t 2 ( 2 )   =   "   1 .   A L L   D I M E N S I O N S   A R E   I N   I N C H E S   U O S "  
 	 	 t e x t 2 ( 3 )   =   "   2 .   C O R N E R S   M U S T   H A V E   F I L L E T S   . 0 0 5   -   . 0 2 0   U O S "  
 	 	 t e x t 2 ( 4 )   =   "   3 .   B R E A K   S H A R P   E D G E S   . 0 0 3   -   . 0 1 5   U O S "  
 	 	 t e x t 2 ( 5 )   =   "   4 .   C H A M F E R S   A R E   . 0 2 0   -   . 0 4 0   U O S "  
 	 	 t e x t 2 ( 6 )   =   "   5 .   D W G   I N T E R P R E T A T I O N S   P E R   A S M E   Y 1 4 . 5 - 2 0 0 9 "  
 	 	 t e x t 2 ( 7 )   =   "   6 .   U O S   A N G U L A R   T O L E R A N C E S   A R E :   < $ t > 5 < $ s > "  
 	 	 t e x t 2 ( 8 )   =   "   7 .   U O S   L I N E A R   T O L E R A N C E S   A R E : . X X X   < $ t > . 0 0 5 , . X X   < $ t > . 0 1 "  
 	 	 t e x t 2 ( 9 )   =   "   8 .   A L L   M A C H I N E D   S U R F A C E S   A R E   1 2 5   R M S   U O S "  
 	 	 t e x t 2 ( 1 0 )   =   "   9 .   A S S O C I A T E D   C O M P U T E R   D A T A   F I L E S   A R E :   "  
 	 	 t e x t 2 ( 1 1 )   =   "                       N X   D W G                 < W @ $ S H _ P A R T _ N A M E > . P R T "  
 	 	 t e x t 2 ( 1 2 )   =   "                       N X   M O D E L             < W @ $ S H _ M A S T E R _ P A R T _ N A M E > . P R T .   "  
 	 	 t e x t 2 ( 1 3 )   =   " 1 0 .   U O S   A L L   < O >   < & 7 0 > < + > < & 1 0 > < + > < O > . 0 1 0 < + > A < + > B < + > C < + > < & 9 0 > 2 3     T H R E A D   P E R   S P E C   P W A   3 5 5 . "  
 	 	 t e x t 2 ( 1 4 )   =   " "  
 	 	 d r a f t i n g N o t e B u i l d e r 1 . T e x t . T e x t B l o c k . S e t T e x t ( t e x t 2 )  
 	 t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 4 ,   " N o t e   -   I n s e r t   T e x t   f r o m   F i l e " )  
 	 t h e S e s s i o n . S e t U n d o M a r k V i s i b i l i t y ( m a r k I d 4 ,   N o t h i n g ,   N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . V i s i b l e )  
 t h e S e s s i o n . S e t U n d o M a r k V i s i b i l i t y ( m a r k I d 1 ,   N o t h i n g ,   N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e )  
 D i m   m a r k I d 5   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 m a r k I d 5   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " N o t e " )  
 t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 5 ,   N o t h i n g )  
 ' D i m   m a r k I d 6   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 ' m a r k I d 6   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " N o t e " )  
 D i m   m a r k I d 7   A s   N X O p e n . S e s s i o n . U n d o M a r k I d   =   N o t h i n g  
 m a r k I d 7   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " N o t e " )  
 D i m   n X O b j e c t 1   A s   N X O p e n . N X O b j e c t   =   N o t h i n g  
 n X O b j e c t 1   =   d r a f t i n g N o t e B u i l d e r 1 . C o m m i t ( )  
 t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 7 ,   N o t h i n g )  
 t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 1 ,   " N o t e " )  
 d r a f t i n g N o t e B u i l d e r 1 . D e s t r o y ( )  
 ' t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 6 ,   N o t h i n g )  
 t h e S e s s i o n . S e t U n d o M a r k V i s i b i l i t y ( m a r k I d 1 ,   N o t h i n g ,   N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . V i s i b l e )  
 	 t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 4 ,   N o t h i n g )  
 '   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
 '       M e n u :   T o o l s - > J o u r n a l - > S t o p   R e c o r d i n g  
 '   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
 E n d   S u b  
 E n d   M o d u l e 
