��/ /   N X   1 1 . 0 . 0 . 3 3  
 / /   J o u r n a l   c r e a t e d   b y   i e v g e n z   o n   F r i   D e c   0 9   1 4 : 4 4 : 2 5   2 0 1 6   E a s t e r n   S t a n d a r d   T i m e  
 / /  
 u s i n g   S y s t e m ;  
 u s i n g   N X O p e n ;  
  
 p u b l i c   c l a s s   N X J o u r n a l  
 {  
     p u b l i c   s t a t i c   v o i d   M a i n ( s t r i n g [ ]   a r g s )  
     {  
         N X O p e n . S e s s i o n   t h e S e s s i o n   =   N X O p e n . S e s s i o n . G e t S e s s i o n ( ) ;  
         N X O p e n . P a r t   w o r k P a r t   =   t h e S e s s i o n . P a r t s . W o r k ;  
         N X O p e n . P a r t   d i s p l a y P a r t   =   t h e S e s s i o n . P a r t s . D i s p l a y ;  
         / /   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
         / /       M e n u :   E d i t - > S e t t i n g s . . .  
         / /   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
         N X O p e n . S e s s i o n . U n d o M a r k I d   m a r k I d 1 ;  
         m a r k I d 1   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . V i s i b l e ,   " S t a r t " ) ;  
          
         N X O p e n . D i s p l a y a b l e O b j e c t [ ]   v i e w l a b e l s 1   =   n e w   N X O p e n . D i s p l a y a b l e O b j e c t [ 1 ] ;  
         N X O p e n . A n n o t a t i o n s . N o t e   n o t e 1   =   ( N X O p e n . A n n o t a t i o n s . N o t e ) w o r k P a r t . F i n d O b j e c t ( " E N T I T Y   2 5   4   1 " ) ;  
         v i e w l a b e l s 1 [ 0 ]   =   n o t e 1 ;  
         N X O p e n . D r a w i n g s . E d i t V i e w L a b e l S e t t i n g s B u i l d e r   e d i t V i e w L a b e l S e t t i n g s B u i l d e r 1 ;  
         e d i t V i e w L a b e l S e t t i n g s B u i l d e r 1   =   w o r k P a r t . S e t t i n g s M a n a g e r . C r e a t e D r a w i n g E d i t V i e w L a b e l S e t t i n g s B u i l d e r ( v i e w l a b e l s 1 ) ;  
          
         t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 1 ,   " S e t t i n g s   D i a l o g " ) ;  
          
         N X O p e n . D r a f t i n g . B a s e E d i t S e t t i n g s B u i l d e r [ ]   e d i t s e t t i n g s b u i l d e r s 1   =   n e w   N X O p e n . D r a f t i n g . B a s e E d i t S e t t i n g s B u i l d e r [ 1 ] ;  
         e d i t s e t t i n g s b u i l d e r s 1 [ 0 ]   =   e d i t V i e w L a b e l S e t t i n g s B u i l d e r 1 ;  
         w o r k P a r t . S e t t i n g s M a n a g e r . P r o c e s s F o r M u l t i p l e O b j e c t s S e t t i n g s ( e d i t s e t t i n g s b u i l d e r s 1 ) ;  
          
         e d i t V i e w L a b e l S e t t i n g s B u i l d e r 1 . V i e w D e t a i l L a b e l . L a b e l P a r e n t D i s p l a y   =   N X O p e n . D r a w i n g s . V i e w D e t a i l L a b e l B u i l d e r . L a b e l P a r e n t D i s p l a y T y p e s . N o t e ;  
          
         N X O p e n . S e s s i o n . U n d o M a r k I d   m a r k I d 2 ;  
         m a r k I d 2   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " S e t t i n g s " ) ;  
          
         t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 2 ,   n u l l ) ;  
          
         N X O p e n . S e s s i o n . U n d o M a r k I d   m a r k I d 3 ;  
         m a r k I d 3   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " S e t t i n g s " ) ;  
          
         N X O p e n . N X O b j e c t   n X O b j e c t 1 ;  
         n X O b j e c t 1   =   e d i t V i e w L a b e l S e t t i n g s B u i l d e r 1 . C o m m i t ( ) ;  
          
         t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 3 ,   n u l l ) ;  
          
         t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 1 ,   " S e t t i n g s " ) ;          
         e d i t V i e w L a b e l S e t t i n g s B u i l d e r 1 . D e s t r o y ( ) ;  
          
         / /   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
         / /       M e n u :   E d i t - > S e t t i n g s . . .  
         / /   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
         N X O p e n . S e s s i o n . U n d o M a r k I d   m a r k I d 4 ;  
         m a r k I d 4   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . V i s i b l e ,   " S t a r t " ) ;  
          
         N X O p e n . D i s p l a y a b l e O b j e c t [ ]   v i e w l a b e l s 2   =   n e w   N X O p e n . D i s p l a y a b l e O b j e c t [ 1 ] ;  
         v i e w l a b e l s 2 [ 0 ]   =   n o t e 1 ;  
         N X O p e n . D r a w i n g s . E d i t V i e w L a b e l S e t t i n g s B u i l d e r   e d i t V i e w L a b e l S e t t i n g s B u i l d e r 2 ;  
         e d i t V i e w L a b e l S e t t i n g s B u i l d e r 2   =   w o r k P a r t . S e t t i n g s M a n a g e r . C r e a t e D r a w i n g E d i t V i e w L a b e l S e t t i n g s B u i l d e r ( v i e w l a b e l s 2 ) ;  
          
         t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 4 ,   " S e t t i n g s   D i a l o g " ) ;  
          
         N X O p e n . D r a f t i n g . B a s e E d i t S e t t i n g s B u i l d e r [ ]   e d i t s e t t i n g s b u i l d e r s 2   =   n e w   N X O p e n . D r a f t i n g . B a s e E d i t S e t t i n g s B u i l d e r [ 1 ] ;  
         e d i t s e t t i n g s b u i l d e r s 2 [ 0 ]   =   e d i t V i e w L a b e l S e t t i n g s B u i l d e r 2 ;  
         w o r k P a r t . S e t t i n g s M a n a g e r . P r o c e s s F o r M u l t i p l e O b j e c t s S e t t i n g s ( e d i t s e t t i n g s b u i l d e r s 2 ) ;  
          
         e d i t V i e w L a b e l S e t t i n g s B u i l d e r 2 . V i e w D e t a i l L a b e l . L a b e l P a r e n t D i s p l a y   =   N X O p e n . D r a w i n g s . V i e w D e t a i l L a b e l B u i l d e r . L a b e l P a r e n t D i s p l a y T y p e s . L a b e l ;  
          
         N X O p e n . S e s s i o n . U n d o M a r k I d   m a r k I d 5 ;  
         m a r k I d 5   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " S e t t i n g s " ) ;  
          
         t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 5 ,   n u l l ) ;  
          
         N X O p e n . S e s s i o n . U n d o M a r k I d   m a r k I d 6 ;  
         m a r k I d 6   =   t h e S e s s i o n . S e t U n d o M a r k ( N X O p e n . S e s s i o n . M a r k V i s i b i l i t y . I n v i s i b l e ,   " S e t t i n g s " ) ;  
          
         N X O p e n . N X O b j e c t   n X O b j e c t 2 ;  
         n X O b j e c t 2   =   e d i t V i e w L a b e l S e t t i n g s B u i l d e r 2 . C o m m i t ( ) ;  
          
         t h e S e s s i o n . D e l e t e U n d o M a r k ( m a r k I d 6 ,   n u l l ) ;  
          
         t h e S e s s i o n . S e t U n d o M a r k N a m e ( m a r k I d 4 ,   " S e t t i n g s " ) ;  
          
         e d i t V i e w L a b e l S e t t i n g s B u i l d e r 2 . D e s t r o y ( ) ;  
          
         / /   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
         / /       M e n u :   T o o l s - > J o u r n a l - > S t o p   R e c o r d i n g  
         / /   - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -  
          
     }  
     p u b l i c   s t a t i c   i n t   G e t U n l o a d O p t i o n ( s t r i n g   d u m m y )   {   r e t u r n   ( i n t ) N X O p e n . S e s s i o n . L i b r a r y U n l o a d O p t i o n . I m m e d i a t e l y ;   }  
 }  
 
