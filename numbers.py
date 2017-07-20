z20 = ['zero','one','two','three','four',
       'five','six','seven','eight','nine',
       'ten','eleven','twelve','thirteen',
       'fourteen','fifteen','sixteen',
       'seventeen','eighteen','nineteen']

zTens = ['twenty','thirty','fourty','fifty',
         'sixty','seventy','eighty','ninety']

zPlaces = ['thousand','million','billion','trillion',
           'quadrillion','quintillion','sextillion','septillion',
           'octillion','nonillion','decillion','undecillion',
           'duodecillion','tredecillion','quattuordecillion',
           'quindecillion','sexdecillion','septendecillion',
           'cctodecillion','novemdecillion','vigintillion','centillion']

def say( z ):
    (neg,z) = ("negative " if z < 0 else "", z if z >= 0 else -z)
    if z < 20:
        return neg + z20[z]
    elif z < 100:
        n = zTens[z//10-2]
        if z%10 > 0:
            n += "-" + z20[z%10]
        return neg + n
    elif z < 1000:
        n = say(z%100) if z%100 != 0 else ""
        hs = z//100
        return neg + z20[hs] + " hundred" + (" and " + n if n != "" else "")
    else:
        i = 0
        n = say(z%1000)
        while z > 0 and i < len(zPlaces):
            z = z//1000
            if z%1000 != 0:
                n = say(z%1000) + " " + zPlaces[i] + (" " + n if n != "" else "")
            i = i+1
        return neg + n