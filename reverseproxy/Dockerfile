FROM nginx

COPY --from=gik25138.live.dynatrace.com/linux/oneagent-codemodules-musl:dotnet / /
ENV LD_PRELOAD /opt/dynatrace/oneagent/agent/lib64/liboneagentproc.so
ENV DT_TENANT=gik25138.live.dynatrace.com
ENV DT_TENANTTOKEN=dt0c01.JQUTDCF6XMGUMUS6XE5WVCK3.RYRGPWFOLJTCOVNWPLYLKTACMBOLVH7P4MG6HDXEZAXP2YTVFKMN2HIVRGZ2HXYU

ARG NGINX_CONF
RUN echo $NGINX_CONF
ENV FILE="${NGINX_CONF}.nginx.conf"
COPY $FILE /etc/nginx/nginx.conf

