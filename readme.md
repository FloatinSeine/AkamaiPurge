AkamaiPurge

===========


A small console app to flush media on the Akamai CDN


Command Line Options
-d = [production | staging] Domain Type 
-t = [cp | arl]		    Purge Type 
-a = [invalidate | remove]  Action Type
-e = [email address]	    Email to notify when complete
-f = [filepath]		    Path to file containing ARLs to Purge. New line for each ARL
-?			    Help

Example
console.exe -d=production -t=arl -a=invalidate -e="recipient@company.com" -f="c:\\path\\arls.txt"
console.exe -?


API Result codes follow this pattern (from the CCUAPI docs):
1xx - Successful Request
2xx - Warning; reserved. The removal request has been accepted.
3xx - Bad or invalid request.
4xx - Contact Akamai Customer Care.